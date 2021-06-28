Option Strict Off
Option Explicit On
Public Class Column
	
	'**-Variable definition. ControlType, this variable is used to know the control type
	'**-(1.- ChekcControl)
	'-Se define la variable "ControlType", para conservar el tipo de Control
	'-(1.- ChekcControl)
	
	Public ControlType As Integer
	Public Title As String
	
    Public FieldName As String
	Public Descript As String
	Public Checked As Short
	Public DefValue As String
	Public OnClick As String
	Public Disabled As Boolean
	Public TabIndex As Short
	
	Public Length As Short
	Public isRequired As Boolean
    Public Alias_Renamed As String
	Public ShowThousand As Boolean
	Public DecimalPlaces As Short
	Public HRefUrl As String
	Public HRefScript As String
	Public OnChange As String
	Public FieldProduct As String
	Public FieldBranch As String
	Public ProdClass As Values.eProdClass
	Public bAllowNegativ As Boolean
	Public Src As String
	
	Public TableName As String
	Public ValuesType As Values.eValuesType
	Public ComboSize As Short
	Public CodeType As Values.eTypeCode
	Public NeedParam As Boolean
	Public MaxLength As Short
	Public FieldClieName As String
	Public FieldCompanyName As String
	Public isDIVDefine As Boolean
	Public Separate As Boolean
	Public EditRecord As Boolean
	
	'UPGRADE_NOTE: Parameters was upgraded to Parameters. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Parameters As Parameters
	Public BlankPosition As Boolean
	
	Public CreateClient As Boolean
	
	Public sCodispl As String
	Public nNotenum As Double
	Public ShowSmallImage As Boolean
	Public bQuery As Boolean
	Public nIndexNotenum As Double
	Public nOriginalNotenum As Double
	Public nCopyNotenum As Double
	
	'**+This property indicates if the column is showed in the Grid
	'+ Estas Propiedades indican si la columna se muestra o no en el grid o la PopUp
	Public GridVisible As Boolean
	Public PopUpVisible As Boolean
	
	Private meTypeList As Values.ecbeTypeList
	Private mstrList As String
	
	'**-Establish the text control area
	'- Establece el área de un control de texto (TextAreaControl)
	Public Rows As Short
	Public Cols As Short
	
	'**-Variables definition. These variables are used to establish the heigh and the width of the images
	'- Variables para ser usadas para estableces el alto y ancho de las imágenes
	Public Width As Integer
	Public Height As Integer
	
	''- Variable que almacena el numero de transacciones
	Public nKeynum As Short
	
	''- Variable para almacenar el una cadena de registros para consultas asociadas
	Public sQueryString As String
	
	'- Se declara propiedad para validar un TexControl que se ocupa como NumericControl
	Public bNumericText As Boolean
	
	'- Se declara propiedad para validar que el valores posibles acepte valores inválidos
	Public bAllowInvalid As Boolean
	
	'- Se declara propiedad para validar que el control de clientes acepte valores inválidos, caso particular de la BC005
	Public bAllowInvalidFormat As Boolean

	'- Indica si se desea mostrar la descripción en los valores posibles
	Public ShowDescript As Boolean
	
	'- Propiedades para el manejo de los parámetros que retorna un valores posibles
	Public sPossiblesVName As String
	Public sParamName As String
	
	'- Se declara propiedad para pasar QueryString a la ventana de clientes
	Public sQueryStringClient As String
	
	'-Variable que indica los clientes a mostrar
	Public nTypeForm As Values.eTypeClient
	
	Public ClientRole As String

    '- Variable para oculatr campos que se requiere esten creados, 0 no se muestra, 100 se muestra normal    
    Public Opacity As Integer = 100
	
	'-Orden de busqueda
	Private meOrder As Values.ecbeOrder
	
	'-Variable que guarda el número de sesión
	Public sSessionID As String
	
	'-Código del usuario
	Public nUsercode As Integer
	
	'-Variable que guarda el digito verificado del cliente
	Public Digit As String
	
	'-Variable para indicar cual es el nombre del campo clave a usar en los valores posibles
	Public KeyField As String
	
	'-Variable que indica que no se desea usar el cache para ese control
	Public NotCache As Boolean
	
	Public CustomPage As String
	
	'***List: This property returns the list of values to be used in the combo control
	'*List: Esta Propiedad devuelve la lista de valores a excluir/incluir en el caso de los combo control
	
	'***List: This property updates the list of values to be used in the combo control
    '%List: Este metodo actualiza el valor de la lista de valores a excluir/incluir en el caso de los combo control

    Public Property List() As String
        Get
            List = mstrList
        End Get
        Set(ByVal Value As String)
            mstrList = Value
        End Set
    End Property

    Public Property sAlias As String
        Get
            Return Alias_renamed
        End Get
        Set(ByVal Value As String)
            Alias_renamed = Value
        End Set
    End Property


	'***TypeList: This property returns the type of list to be used in the combo control
	'*TypeList: Esta Propiedad devuelve el tipo de validacion a aplicar a la lista de valores en el "Combo control"
	
	'***TypeList: This property updates the type of list to be used in the combo control
	'*TypeList: Esta Propiedad update el tipo de validacion a aplicar a la lista de valores en el "Combo control"
	Public Property TypeList() As Values.ecbeTypeList
		Get
			TypeList = meTypeList
		End Get
		Set(ByVal Value As Values.ecbeTypeList)
			meTypeList = Value
		End Set
	End Property
	
	'*** TypeOrder: indicates the order in which the combo box values are going to be charged.
	'* TypeOrder: indica el orden en que se van a cargar los valores de los combos
	
	'**% TypeOrder: indicates the order in which the combo box values are going to be charged.
	'% TypeOrder: indica el orden en que se van a cargar los valores de los combos
	Public Property TypeOrder() As Values.ecbeOrder
		Get
			TypeOrder = meOrder
		End Get
		Set(ByVal Value As Values.ecbeOrder)
			meOrder = Value
		End Set
	End Property
	
	'**%Class_Initialize: Controls the creation of an instance of the class
	'%Class_Initialize: Controla la creación de una instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		EditRecord = False
		GridVisible = True
		PopUpVisible = True
		BlankPosition = True
        Opacity = 100
		TypeOrder = Values.ecbeOrder.Descript
		Parameters = New Parameters
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






