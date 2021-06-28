Option Strict Off
Option Explicit On
Option Compare Text
Public Class Err_Comp
	
	'+
	'+ Estructura de tabla err_comp al 10-04-2002 13:41:40
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nSeq As Integer ' NUMBER     22   0     10   N
	Public nErrorNum As Integer ' NUMBER     22   0     10   N
	Public nId As Integer ' NUMBER     22   0     5    N
	Public nCompType As eComponType ' NUMBER     22   0     5    N
	Public sCompName As String ' VARCHAR2   25   0     0    N
	Public sCompPath As String ' VARCHAR2   50   0     0    S
	Public nCompVers As Integer ' NUMBER     22   0     5    S
	Public dToQC As Date ' DATE       7    0     0    S
	Public dToQA As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	
	'-Tipos de componentes
	Public Enum eComponType
		eComTypWeb = 1
		eComTypDLL = 2
		eComTypSP = 3
		eComTypRep = 4
		eComTypOther = 5
	End Enum
	
	
	
	'%InsUpdErr_Comp: Se encarga de actualizar la tabla Err_Comp
	Private Function InsUpdErr_Comp(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpderr_comp As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		lrecinsUpderr_comp = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpderr_comp al 10-04-2002 13:49:33
		'+
		With lrecinsUpderr_comp
			.bErr_Module = True
			.StoredProcedure = "insUpderr_comp"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nErrorNum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nComptype", nCompType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCompname", sCompName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sComppath", sCompPath, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompvers", nCompVers, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dToqc", dToQC, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dToQA", dToQA, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeq", nSeq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdErr_Comp = .Run(False)
			
			'+Se recupera indice asignado a nuevo registro
			If InsUpdErr_Comp Then
				nSeq = .Parameters("nSeq").Value
			End If
			
		End With
		
		lrecinsUpderr_comp = Nothing
		
		Exit Function
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		If Not IsIDEMode Then
		End If
		
		Add = InsUpdErr_Comp(1)
		
		Exit Function
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		If Not IsIDEMode Then
		End If
		
		Update = InsUpdErr_Comp(2)
		
		Exit Function
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		If Not IsIDEMode Then
		End If
		
		Delete = InsUpdErr_Comp(3)
		
		Exit Function
	End Function
	
	'%Find: Lee los datos de la tabla
    Public Function Find(ByVal nErrorNum As Integer, ByVal nId As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaErr_comp_err As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If
        lrecreaErr_comp_err = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure reaErr_comp_err al 10-04-2002 13:43:26
        '+
        With lrecreaErr_comp_err
            .bErr_Module = True
            .StoredProcedure = "reaErr_comp_err"
            .Parameters.Add("nErrorNum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                Find = True
                Me.nErrorNum = nErrorNum
                Me.nId = nId
                Me.nSeq = .FieldToClass("nSeq")
                Me.nCompType = .FieldToClass("nComptype")
                Me.sCompName = .FieldToClass("sCompname")
                Me.sCompPath = .FieldToClass("sComppath")
                Me.nCompVers = .FieldToClass("nCompvers")
                Me.dToQC = .FieldToClass("dToQC")
                Me.dToQA = .FieldToClass("dToQA")
                Me.nUsercode = .FieldToClass("nUsercode")
                .RCloseRec()
            Else
                Find = False
            End If
        End With

        lrecreaErr_comp_err = Nothing

        Exit Function
    End Function
	
	'%InsValER005: Validaciones de la transacción(Folder)
	Public Function InsValER005(ByVal sCodispl As String, ByVal sAction As String, ByVal nErrorNum As Integer, ByVal nId As Integer, ByVal nCompType As Integer, ByVal sCompName As String, ByVal sCompPath As String, ByVal nCompVers As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		If Not IsIDEMode Then
		End If
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nErrorNum = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("ER005", 700029,  , eFunctions.Errors.TextAlign.RigthAling, ": ", 1135)
			End If
			'        If nId = NumNull Then
			'            Call .ErrorMessage("ER005", 700029, , RigthAling, ": ", 1136)
			'        End If
			If nCompType = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("ER005", 700029,  , eFunctions.Errors.TextAlign.RigthAling, ": ", 1137)
			End If
			If sCompName = String.Empty Then
				Call .ErrorMessage("ER005", 700029,  , eFunctions.Errors.TextAlign.RigthAling, ": ", 1138)
			End If
			If (nCompType = eComponType.eComTypWeb Or nCompType = eComponType.eComTypRep) Then
				If sCompPath = String.Empty Then
					Call .ErrorMessage("ER005", 700029,  , eFunctions.Errors.TextAlign.RigthAling, ": ", 1139)
				End If
			End If
			If nCompVers = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("ER005", 700029,  , eFunctions.Errors.TextAlign.RigthAling, ": ", 1140)
			End If
			
			InsValER005 = .Confirm
		End With
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'%InsPostER005: Ejecuta el post de la transacción
	Public Function InsPostER005(ByVal sAction As String, ByVal nErrorNum As Integer, ByVal nId As Integer, ByVal nCompType As Integer, ByVal sCompName As String, ByVal sCompPath As String, ByVal nCompVers As Integer, ByVal dToQC As Date, ByVal dToQA As Date, ByVal nUsercode As Integer) As Boolean
		If Not IsIDEMode Then
		End If
		
		With Me
			.nErrorNum = nErrorNum
			.nId = nId
			.nCompType = nCompType
			.sCompName = sCompName
			.sCompPath = sCompPath
			.nCompVers = nCompVers
			.dToQC = dToQC
			.dToQA = dToQA
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostER005 = Add
			Case "Update"
				InsPostER005 = Update
			Case "Del"
				InsPostER005 = Delete
		End Select
		
		Exit Function
	End Function
	
	'%Class_Initialize: Inicializa el objeto
	Private Sub Class_Initialize_Renamed()
		If Not IsIDEMode Then
		End If
		
		nSeq = eRemoteDB.Constants.intNull
		nErrorNum = eRemoteDB.Constants.intNull
		nId = eRemoteDB.Constants.intNull
		nCompType = eRemoteDB.Constants.intNull
		sCompName = String.Empty
		sCompPath = String.Empty
		nCompVers = eRemoteDB.Constants.intNull
		dToQC = eRemoteDB.Constants.dtmNull
		dToQA = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class











