/// The VERGE 3 Project is originally by Ben Eirich and is made available via
///  the BSD License.
///
/// Please see LICENSE in the project's root directory for the text of the
/// licensing agreement.  The CREDITS file in the same directory enumerates the
/// folks involved in this public build.
///
/// If you have altered this source file, please log your name, date, and what
/// changes you made below this line.


#ifndef _OPCODES_H_
#define _OPCODES_H_

// Opcode values.
#define ERROR				0
#define opRETURN			1
#define opASSIGN			2
#define opIF				3
#define opGOTO				4
#define opSWITCH			5
#define opCASE				6
#define opDEFAULT			7
#define opLIBFUNC			8
#define opUSERFUNC			9
#define opRETVALUE			10
#define opRETSTRING			11

// Overkill: Variadic functions
#define opVARARG_START		12 // Start of argument list
#define opVARARG_END		13 // End of argument list

#define ifZERO				20
#define	ifNONZERO			21
#define ifEQUAL				22
#define ifNOTEQUAL			23
#define ifGREATER			24
#define ifGREATEROREQUAL	25
#define ifLESS				26
#define ifLESSOREQUAL		27
#define ifAND				28
#define ifOR				29
#define ifUNGROUP			30

#define iopADD				31
#define iopSUB				32
#define iopDIV				33
#define iopMULT				34
#define iopMOD				35
#define iopSHL				36
#define iopSHR				37
#define iopAND				38
#define iopOR				39
#define iopXOR				40
#define iopEND				41
#define iopNOT				42
#define iopNEGATE			43

#define intLITERAL			50
#define intHVAR0			51
#define intHVAR1			52
#define intGLOBAL			53
#define intARRAY			54
#define intLOCAL			55
#define intLIBFUNC			56
#define intUSERFUNC			57
#define intGROUP			58

#define sADD				60
#define sEND				61

#define strLITERAL			70
#define strGLOBAL			71
#define strARRAY			72
#define strLOCAL			73
#define strLIBFUNC			74
#define strUSERFUNC			75
#define strHSTR0            76
#define strHSTR1			77
#define strINT				78
#define strLEFT				79
#define strRIGHT			80
#define strMID				81

#define aSET				90
#define aINC				91
#define aDEC				92
#define aINCSET				93
#define aDECSET				94

//plugin API

#define	intPLUGINVAR		100
#define strPLUGINVAR		101
#define	intPLUGINFUNC		102
#define strPLUGINFUNC		103
#define opPLUGINFUNC		104

// Callbacks
#define opRETCB				105 // Function returning a callback.
#define opCBINVOKE			106 // Callback invocation with no return value
#define opCBCOPY			107 // When copying a callback from a variable.
#define strCBINVOKE			108 // Callback invocation with string return value
#define intCBINVOKE			109 // Callback invocation with int return value
#define cbLOCAL				110 // Local callback
#define cbGLOBAL			111 // Global callback
#define cbARRAY				112 // Global callback array
#define cbUSERFUNC			113 // A function that returns a callback.
#define opCBPADDING			114 // Needed after call attempt, so that failure won't mess the interpreter.
#define opCBFUNCEXISTS		115	// Test if a function exists or not.

// types are 1 = int, 3 = string, 4 = variable number of ints, 5 = void, 6 = struct

#define t_NOTFOUND			0 // Reserved for errors.
#define t_INT				1
#define t_STRING			3
#define t_VARARG			4
#define t_VOID				5
#define t_STRUCT			6
#define t_BOOL				7 // For better  lua compatibility with builtins.
#define t_CALLBACK			8 // For function pointers.

#define NUM_LIBFUNCS		233
#define NUM_HVARS			132
#define NUM_HDEFS			112

struct VcFunctionDecl {
	const char* returnType;
	const char* name;
	const char* argumentTypes;
};

class Define
{
public:
	enum Type {
		Type_Raw,
		Type_PleaseQuote
	} type;
	std::string key, value;
	Define(const char *k, const char *v, Type type = Type_Raw);
	~Define();
};

struct HdefDecl {
	const char* key;
	const char* value;
	Define::Type type;
};

void vc_initBuiltins();
void vc_initLibrary();

struct VcFunction {
	int returnType;
	std::string name;
    std::vector<int> argumentTypes;

	VcFunction() {}

	VcFunction(VcFunctionDecl &decl)
		: returnType(decl.returnType?atoi(decl.returnType):0),
		name(decl.name?decl.name:"")
	{
		const char * at = decl.argumentTypes;
		if(!at) return;

		char buf[2];
		memset(buf, 0, 2);
		while (*at) {
			buf[0] = *at;
			int argumentType = atoi(buf);
			argumentTypes.push_back(argumentType);
			at++;
		}
	}
};

typedef void (*VcFunctionImpl) ();
typedef std::map<std::string, VcFunctionImpl> VcFunctionImplTable;
typedef std::map<int, VcFunctionImpl> VcFunctionDispatchTable;

extern VcFunctionImplTable & VcGetLibfuncBindings ();

struct VcFunctionBindingDecl {
	const char * name;
	VcFunctionImpl fn;
};

extern VcFunctionBindingDecl _bind_decl[1000];
extern int _bind_decl_ctr;

struct VcFunctionBinding {
	VcFunctionBinding(const char* name, const VcFunctionImpl& fn) {
		_bind_decl[_bind_decl_ctr].name = name;
		_bind_decl[_bind_decl_ctr++].fn = fn;
	}
};

extern VcFunction* libfuncs;
extern VcFunctionImplTable implTable;
extern VcFunctionDispatchTable dispatchTable;
extern char* libvars[NUM_HVARS][3];
extern HdefDecl hdefs[NUM_HDEFS];

#endif  /* OPCODES_H */
