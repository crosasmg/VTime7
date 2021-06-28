Option Strict Off
Option Explicit On
Public Class report_prods
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: report_prods.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Variable local para contener colección.
	
	Private mCol As Collection
	
	'+ Se definen las propiedades auxiliares.
	
	Private mintBranch As Integer
	Private mintProduct As Integer
	Private mdtmEffecdate As Date
	Private mstrCodCodispl As String
	'% AddReport_prod: Este método permite añadir registros a la colección.
    Public Function AddReport_prod(ByRef sCodCodispl As String, ByRef sCodCodispldes As String, ByRef nTypeReport As Integer, _
                                   ByRef nRepType As Long, ByRef nTratypep As Long, ByRef sReport As String, ByRef sDesRepType As String, ByRef sDesTratypep As String) As report_prod
        '+ Crear un nuevo objeto.
        Dim objNewMember As report_prod
        objNewMember = New report_prod
        '+ Establecer las propiedades que se transfieren al método.
        With objNewMember
            .sCodCodispl = sCodCodispl
            .sDescript = sCodCodispldes
            .nType_Report = nTypeReport
            .nRepType = nRepType
            .nTratypep = nTratypep
            .sReport = sReport
            .sDesRepType = sDesRepType
            .sDesTratypep = sDesTratypep
        End With
        mCol.Add(objNewMember)
        '+ Return the object created.
        AddReport_prod = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function
	
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As report_prod
		Get
			'+ Se usa al hacer referencia a un elemento de la colección
			'+ vntIndexKey contiene el índice o la clave de la colección,
			'+ por lo que se declara como un Variant Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5).
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'+ Se usa al obtener el número de elementos de la colección. Sintaxis: Debug.Print x.Count.
			
			Count = mCol.Count()
		End Get
	End Property
	
    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mCol.GetEnumerator
    End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'+ Se usa al quitar un elemento de la colección vntIndexKey contiene el índice o la clave,
		'+ por lo que se declara como un Variant Sintaxis: x.Remove(xyz).
		
		mCol.Remove(vntIndexKey)
	End Sub
	
    Private Sub Class_Initialize_Renamed()
        '+ Crea la colección cuando se crea la clase.

        mCol = New Collection
    End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
    Private Sub Class_Terminate_Renamed()
        '+ Destruye la colección cuando se termina la clase.

        mCol = Nothing
    End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% FindReport_prod: Verifica que exista información en la tabla de conmutativos.
	Public Function FindReport_prod(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaReport_prod As eRemoteDB.Execute
		
		lrecReaReport_prod = New eRemoteDB.Execute
		
		On Error GoTo FindReport_prod_Err
		
		FindReport_prod = True
		
		If nBranch <> mintBranch Or nProduct <> mintProduct Or dEffecdate <> mdtmEffecdate Or lblnFind Then
            mCol = Nothing
			mCol = New Collection
			
			'+ Definición de parámetros para stored procedure 'insudb.reaConmutativ'.
			
			With lrecReaReport_prod
				.StoredProcedure = "reaReport_prod"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					mintBranch = nBranch
					mintProduct = nProduct
					mdtmEffecdate = dEffecdate
					Do While Not .EOF
                        Call AddReport_prod(.FieldToClass("sCodispl"), .FieldToClass("sDescript"), .FieldToClass("nType_report"), _
                                            .FieldToClass("nRepType"), .FieldToClass("nTratypep"), .FieldToClass("sReport"), _
                                            .FieldToClass("sDesRepType"), .FieldToClass("sDesTratypep"))
						.RNext()
					Loop 
					.RCloseRec()
				Else
					FindReport_prod = False
					mintBranch = 0
					mintProduct = 0
					mdtmEffecdate = CDate(Nothing)
				End If
			End With
            lrecReaReport_prod = Nothing
		End If
		
FindReport_prod_Err: 
		If Err.Number Then
			FindReport_prod = False
		End If
		On Error GoTo 0
	End Function
	
	
	'% Find: Verifica que exista información en la tabla de conmutativos.
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sCodCodispl As String, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaReport_prod As eRemoteDB.Execute
		
		lrecReaReport_prod = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		Find = True
		
        If nBranch <> mintBranch Or nProduct <> mintProduct Or sCodCodispl <> mstrCodCodispl Or dEffecdate <> mdtmEffecdate Or lblnFind Then
            mCol = Nothing
            mCol = New Collection

            '+ Definición de parámetros para stored procedure 'insudb.reaConmutativ'.

            With lrecReaReport_prod
                .StoredProcedure = "reaReport_prod_cod"
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sCodCodispl", sCodCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    mintBranch = nBranch
                    mintProduct = nProduct
                    mstrCodCodispl = sCodCodispl
                    mdtmEffecdate = dEffecdate
                    Do While Not .EOF
                        Call AddReport_prod(.FieldToClass("sCodispl"), .FieldToClass("sDescript"), .FieldToClass("nType_report"), _
                                            .FieldToClass("nRepType"), .FieldToClass("nTratypep"), .FieldToClass("sReport"), _
                                            .FieldToClass("sDesRepType"), .FieldToClass("sDesTratypep"))
                        .RNext()
                    Loop
                    .RCloseRec()
                Else
                    Find = False
                    mintBranch = 0
                    mintProduct = 0
                    mstrCodCodispl = CStr(Nothing)
                    mdtmEffecdate = CDate(Nothing)
                End If
            End With
            lrecReaReport_prod = Nothing
        End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function

    '% FindReport_prod_By_Transac: Reportes automáticos por tipo de transacción.
    Public Function FindReport_prod_By_Transac(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nTransaction As Integer, ByVal nRepType As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = True) As Boolean
        Dim lrecReaReport_prod As eRemoteDB.Execute
        Dim nTratypep As Integer

        lrecReaReport_prod = New eRemoteDB.Execute

        On Error GoTo FindReport_prod_By_Transac_Err

        FindReport_prod_By_Transac = True

        If nBranch <> mintBranch Or nProduct <> mintProduct Or dEffecdate <> mdtmEffecdate Or lblnFind Then
            mCol = Nothing
            mCol = New Collection

            '+ Definición de parámetros para stored procedure 'insudb.reaConmutativ'.
            Select Case nTransaction
                Case 1, 2, 3, 18, 19, 45, 30, 31
                    nTratypep = 1
                Case 12, 13, 14, 15, 26, 27
                    nTratypep = 2
                Case 8, 9, 10, 11, 44
                    nTratypep = 3
                    '+Declaraciones
                Case 21
                    nTratypep = 4
                Case 4, 5, 24, 25, 39, 28, 29, 41
                    nTratypep = 6
                Case 6, 7, 40, 42, 34, 43, 23
                    nTratypep = 7
            End Select

            With lrecReaReport_prod
                .StoredProcedure = "reaReport_prod_By_Transac"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nRepType", nRepType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    mintBranch = nBranch
                    mintProduct = nProduct
                    mdtmEffecdate = dEffecdate
                    Do While Not .EOF
                        Call AddReport_prod(.FieldToClass("sCodispl"), .FieldToClass("sDescript"), .FieldToClass("nType_report"), _
                                            .FieldToClass("nRepType"), .FieldToClass("nTratypep"), .FieldToClass("sReport"), _
                                            .FieldToClass("sDesRepType"), .FieldToClass("sDesTratypep"))
                        .RNext()
                    Loop
                    .RCloseRec()
                Else
                    FindReport_prod_By_Transac = False
                    mintBranch = 0
                    mintProduct = 0
                    mdtmEffecdate = CDate(Nothing)
                End If
            End With
            lrecReaReport_prod = Nothing
        End If

FindReport_prod_By_Transac_Err:
        If Err.Number Then
            FindReport_prod_By_Transac = False
        End If
        On Error GoTo 0
    End Function

End Class






