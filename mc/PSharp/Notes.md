PSharp – Version Alpha 0.1.0 Release Notes
===========================================

Project Overview
----------------
PSharp is a compiler front‑end for the P# language, built from scratch in C#.  
This first alpha release establishes the core infrastructure, closely mirroring the architecture of the Roslyn compiler platform.

Key Components
--------------

### 1. Lexer (Scanner)
- Produces a stream of tokens (SyntaxToken) from source text.
- Token kinds are defined in a generated SyntaxKind enum.
- Follows Roslyn’s design: tokens carry a green token (GreenToken) that holds raw text, value, and trivia.
- Supports numeric, string, character, boolean, and keyword literals, as well as operators and punctuation.
- Lexer errors are collected in a diagnostic bag.

### 2. Syntax Tree – Red/Green Model
Inspired by Roslyn, the syntax tree is split into two layers:

- **Green nodes**: Immutable, parent‑less, and store only the structural information (children, tokens, trivia).  
  Each green node type is generated from an XML definition and implements `GetSlot` and `SlotCount`.

- **Red nodes**: Wrap green nodes, add parent references, absolute positions, and lazy creation of children.  
  They are the public API used by the parser and later phases.

All syntax node classes (both green and red) are **automatically generated** from XML descriptions.  
The generator reads `SyntaxNode` elements and emits code into `GeneratedSyntax/`.

### 3. Parser
- Hand‑written recursive‑descent parser with precedence climbing for expressions.
- Uses a sliding‑window token buffer (managed by `_lexedTokens`) to support arbitrary lookahead.
- Token management logic has been debugged and now correctly shifts/resizes the buffer without losing tokens.
- Expression parsing relies on the new **ExpressionFactory** (generated) to instantiate red nodes, removing repetitive `new` calls from the parser code.
- Statement parsing remains manual (too complex to generate at this stage).
- Parser errors are added to the diagnostic bag.

### 4. Binder (Semantic Analysis)
- Under construction, but follows Roslyn’s pattern: a separate bound tree with its own node hierarchy.
- Performs name resolution, type inference, and overload resolution.
- Binder produces a fully typed bound tree, ready for lowering or emission.
- Diagnostics from binding are collected and reported.

### 5. Code Generators
Two generators are part of the build:

- **SyntaxNodeGenerator**:  
  Input: XML files describing each syntax node (name, base, kind, category, properties, computed properties).  
  Output: green node classes (`GreenXxx`) and red node classes (`XxxSyntax`) in appropriate namespaces.

- **ExpressionFactoryGenerator**:  
  Reads the same XML and emits a static `ExpressionFactory` class with:
  - One factory method per non‑literal expression (e.g., `Add`, `LogicalNot`, `SimpleAssignment`).
  - A unified `Literal(SyntaxToken token)` method that dispatches on token kind to create the correct literal node.
  - For binary/unary/assignment expressions, the factory uses the `OperatorKind` attribute from the XML to map token kinds to node types.

### 6. XML Grammar Enhancements
To support generation of factories and interfaces, the XML has been extended with:

- `Interfaces` attribute – e.g., `IBinaryExpression`, `IUnaryExpression`, `ILiteralExpression`, `IAssignmentExpression`, `ILoopStatement`, `IJumpStatement`, `ISwitchLabel`.  
  These enable grouping and logical classification.

- `OperatorKind` attribute – e.g., `PlusToken`, `MinusToken`, `AmpersandAmpersandToken`.  
  Specifies which token kind triggers the construction of that expression node.

- `Nullable="true"` – marks optional children (e.g., `ElseClause` in `IfStatementSyntax`), leading to nullable property types and guarded getters.

- `Category` – organises generated files into subfolders (`Expressions`, `Statements`, etc.).

### 7. Build Integration
- The generator lives in a separate project (`PSharp.Generator`).
- The main project (`PSharp`) includes a **pre‑build event** that runs the generator before compilation, ensuring all generated files are up to date.
- Generated files are **committed to source control** to allow the IDE to see them immediately (alternative approach).
- During development, red squiggles may appear until the first successful build – this is expected when using external generators.

### 8. Current Status
- ✅ Lexer – complete for all required tokens.
- ✅ Syntax node generation – **~50 expression nodes** are fully generated and compile.
- 🔶 Statement nodes – partially generated; some manual adjustments remain.
- ✅ Parser – expression parsing uses generated factory; statement parsing is manual.
- 🚧 Binder – under active development (basic scaffolding in place).
- ✅ Build – project compiles without errors.
- ✅ Sample input – can be parsed into a syntax tree; basic round‑trip testing passes.

### 9. Known Issues / Caveats
- **IDE0130 warning** (“Namespace does not match folder structure”) may appear because the generator hard‑codes namespaces; this warning can be safely ignored or suppressed.
- **Pre‑build event** adds a few seconds to each build; an alternative is to run the generator manually only when XML changes.
- The token buffer shift logic, though fixed, may still need further testing with very deep lookahead scenarios.
- Binder is not yet feature‑complete; many semantic rules are missing.

---

*PSharp – version alpha 0.1.0 – February 2025*