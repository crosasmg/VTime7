Option Strict Off
Option Explicit On
Public Module Constants
    '**+Objective:
    '**+Version: $$Revision: $
    '+Objetivo:
    '+Version: $$Revision: $

    '**-Objective: Constant that indicates that an integer type value is assigned a null for passing as a parameter
    '-Objetivo: Constante que indica que a un valor de tipo entero se le asigna null para pasarlo como parametro
    Public Const intNull As Short = -32768

    '**-Objective: Constant that indicates that a date type value is assigned a null for passing it as a parameter
    '-Objetivo: Objective: Constante que indica que a un valor de tipo fecha se le asigna null para pasarlo como parametro
    Public Const dtmNull As Date = Nothing

    '**-Objective: Constant that indicates that a decimal type value is assigned a null for passing it as a parameter
    '-Objetivo: Objective: Constante que indica que a un valor de tipo decimal se le asigna null para pasarlo como parametro
    Public Const dblNull As Double = -32768.3276

    '**-Objective: Constant that indicates that a string type value is assigned a null for passing it as a parameter
    '-Objetivo: Constante que indica que a un valor de tipo cadena se le asigna null para pasarlo como parametro
    Public Const strNull As String = ""

    '**-Objective: Possible actions to execute on the registries of a table
    '-Objetivo: Posibles acciones a ejecutar sobre los registros de una tabla
    Public Enum eActions
        Add = 1
        Update = 2
        Delete = 3
    End Enum
End Module






