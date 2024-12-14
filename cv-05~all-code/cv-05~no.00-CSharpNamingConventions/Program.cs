
public class ClassPascalCase {
	public int PublicFieldPascalCase;
	public int PublicPropertyPascalCase { get; set; }

	public static int PublicStaticFieldPascalCase;
	public static int PublicStaticPropertyPascalCase { get; set; }

	public const int PublicConstantPascalCase = 42;
	
	public int GetVerbPublicMethodPascalCase(int camelCaseArgumentOne) {
		int localVariableCamelCase = 5;
		return 1 + localVariableCamelCase + camelCaseArgumentOne + _privateField + s_privateStaticField;
	}

	private int GetVerbPrivateMethodPascalCase(int camelCaseArgumentTwo) {
		return 2;
	}

	private int PrivatePropertyPascalCase { get; set; }
	private const int PrivateConstantPascalCase = 42;

	private int _privateField;
	private static int s_privateStaticField;	
}

public struct StructPascalCase {
	public int PublicField;
}

public interface IPascalCase {
	public void StartVerbPascalCase();
}

public record class RecordPascalCase(int PascalCaseProperty, long PascalCaseOtherProperty);

public class ClassPrimaryConstructor(int ctorArgumentCamelCase, int _capturedCtorArgument) {
	public int PublicReadOnlyProperty { get; } = ctorArgumentCamelCase; // We do not use ctorArgumentCamelCase
																		// outside of initializations of non-record,
																		// so it does NOT get captured by copy
																		// into a private field.

	public int CalcPublicValue() {
		return _capturedCtorArgument;   // We use _capturedCtorArgument here outside of initializations of non-record,
										// so the name "_capturedCtorArgument" represents the private field
										// with the captured value of primary ctor argument here !!!
	}
}

public class SomeClassTests {
	public void MethodTestPascalCase_PascalCase_PascalCase() {

	}
}