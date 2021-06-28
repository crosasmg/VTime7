Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Public Class Instructions
	Implements System.Collections.IEnumerable
	
	'variable local para contener colección
	Private mCol As Collection
	
	Private mColMacros As Macros
	
	'% insLoadInfoFile : Carga a la colección los datos del script
	Public Function insLoadScript(ByVal sPathScript As String, Optional ByVal bExecute As Boolean = True) As Boolean
        Dim lobjTextStream As IO.StreamReader
		Dim strLine As String
		Dim strActual As String
		Dim strNueva As String
		Dim intIndex As Short
		Dim lintCount As Short
        Dim sngTimer As Single
		
		'+ Indica si el elemento a evaluar de la colección es el primero.
		Dim blnFirst As Boolean
		
		blnFirst = True
		
		mCol = New Collection
        lobjTextStream = IO.File.OpenText(sPathScript)


		insLoadScript = False
		
		strActual = String.Empty
		strNueva = String.Empty
		
		With lobjTextStream
			lintCount = 1
            Do While Not .EndOfStream
                strLine = .ReadLine
                If strLine > String.Empty Then
                    Select Case Mid(strLine, 1, 1)
                        '+ Línea con comentarios
                        Case ";"
                            '+ Línea con definición de parámetros
                        Case "#"
                            General.mclsInstruction.AddDefParameter(strLine)
                            '+ Línea con definición de macro
                        Case "<"

                            General.mclsInstruction.oMacros.Add(strLine)

                        Case "@"
                            General.mclsInstruction.Options(strLine)
                            '+ Línea con código o comando a ejecutar
                        Case Else

                            '+ Cada vez que se ejecuta un nuevo comando se ejecuta el anterior (o en proceso)
                            '+ y se actualizan los datos de performance que corresponden con el elemento de la colección.
                            If Not blnFirst Then
                                sngTimer = VB.Timer()
                                General.mclsInstruction.oExpandMacros = mColMacros
                                If bExecute Then General.mclsInstruction.Execute()
                                '+ Si ocurrió algun error se obtienen sus dados
                                If General.mTypeError.nError > 0 Then
                                    General.mclsInstruction.nError = General.mTypeError.nError
                                    General.mclsInstruction.sDescription = General.mTypeError.sDescription
                                    General.mclsInstruction.blnError = True
                                    General.mTypeError.nError = 0
                                Else
                                    General.mclsInstruction.blnError = False
                                End If
                                General.mclsInstruction.nLast = CDbl(Format(VB.Timer() - sngTimer, "###0.000"))
                                General.mclsInstruction.nSample = General.mclsInstruction.nLast
                            End If

                            intIndex = InStr(strLine, " ")
                            strNueva = Mid(strLine, 1, intIndex - 1)
                            strLine = Trim(Mid(strLine, intIndex + 1))
                            If strActual > String.Empty Then
                                'TODO: DiffTime no esta implementado de forma directa en .Net
                                General.mclsInstruction.nDelay = 0 'CDbl(Format(DiffTime(strActual, strNueva), "###0.000"))
                            End If


                            General.mclsInstruction = Add("M" & lintCount)
                            General.mclsInstruction.sCommand = strLine

                            '+ Establece la función a verdadero siempre y cuando aun no lo sea ya.
                            If Not insLoadScript Then insLoadScript = True

                            strActual = strNueva
                            blnFirst = False
                    End Select
                End If
                lintCount = lintCount + 1
            Loop
			.Close()
		End With

	End Function
	
	Public Function Add(ByRef Key As String, Optional ByRef sKey As String = "") As Instruction
		'crear un nuevo objeto
		Dim objNewMember As Instruction
		objNewMember = New Instruction
		
		
		'establecer las propiedades que se transfieren al método
		objNewMember.Key = Key
		If Len(sKey) = 0 Then
			mCol.Add(objNewMember)
		Else
			mCol.Add(objNewMember, sKey)
		End If
		
		
		'devolver el objeto creado
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Instruction
		Get
			'se usa al hacer referencia a un elemento de la colección
			'vntIndexKey contiene el índice o la clave de la colección,
			'por lo que se declara como un Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	
	Public ReadOnly Property Count() As Integer
		Get
			'se usa al obtener el número de elementos de la
			'colección. Sintaxis: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'esta propiedad permite enumerar
			'esta colección con la sintaxis For...Each
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'se usa al quitar un elemento de la colección
		'vntIndexKey contiene el índice o la clave, por lo que se
		'declara como un Variant
		'Sintaxis: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	Private Function ExecuteCommand() As Boolean
        General.mclsInstruction.oExpandMacros = General.oExpandMacros
        ExecuteCommand = General.mclsInstruction.Execute
	End Function
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'crea la colección cuando se crea la clase
		mCol = New Collection
		mColMacros = New Macros
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'destruye la colección cuando se termina la clase
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		'UPGRADE_NOTE: Object mColMacros may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mColMacros = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






