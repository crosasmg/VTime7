Option Strict Off
Option Explicit On
Public Class Health
	'%-------------------------------------------------------%'
	'% $Workfile:: Health.cls                               $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Description taken on February 07, 2000 of the Health - Medical attention particular data table.
	'-Descripción tomada la fecha 07/02/2000 de la tabla Health - Datos particulares de Atención médica
	
	'Column_name                      Type      Computed   Length  Prec  Scale Nullable   TrimTrailingBlanks   FixedLenNullInSource
	'----------------------------- ----------- ---------- -------- ----- ----- --------- -------------------- ----------------------
	Public nProduct As Integer 'smallint      no        2       5     0     no             (n/a)                (n/a)
	Public nBranch As Integer 'smallint      no        2       5     0     no             (n/a)                (n/a)
	Public sCertype As String 'char          no        1                   no             yes                  no
	Public nPolicy As Double 'int           no        4       10    0     no             (n/a)                (n/a)
	Public nCertif As Double 'int           no        4       10    0     no             (n/a)                (n/a)
	Public dEffecdate As Date 'datetime      no        8                   no             (n/a)                (n/a)
	Public nCapital As Double 'decimal       no        9       18    6     yes            (n/a)                (n/a)
	Public dExpirdat As Date 'datetime      no        8                   yes            (n/a)                (n/a)
	Public sClient As String 'char          no        14                  yes            yes                  yes
	Public nGroup_comp As Integer 'smallint      no        2       5     0     yes            (n/a)                (n/a)
	Public dIssuedat As Date 'datetime      no        8                   yes            (n/a)                (n/a)
	Public nNullcode As Integer 'smallint      no        2       5     0     yes            (n/a)                (n/a)
	Public dNulldate As Date 'datetime      no        8                   yes            (n/a)                (n/a)
	Public nPremium As Double 'decimal       no        9       10    2     yes            (n/a)                (n/a)
	Public dStartDate As Date 'datetime      no        8                   yes            (n/a)                (n/a)
	Public nTariff As Integer 'smallint      no        2       5     0     yes            (n/a)                (n/a)
	Public nTransactio As Integer 'smallint      no        2       5     0     yes            (n/a)                (n/a)
	Public nGroup As Integer 'smallint      no        2       5     0     yes            (n/a)                (n/a)
	Public nRole As Integer 'smallint      no        2       5     0     yes            (n/a)                (n/a)
	Public nSituation As Integer 'smallint      no        2       5     0     yes            (n/a)                (n/a)
	Public nUsercode As Integer 'smallint      no        2       5     0     yes            (n/a)                (n/a)
	
	Public nBenef_type As Integer 'smallint  no       2      5    0     yes      (n/a)              (n/a)
	Public sDefaulti As String 'char      no       1                 yes      yes                yes
	Public nDed_amount As Double 'decimal   no       9      12   0     yes      (n/a)              (n/a)
	Public nLimit As Double 'decimal   no       9      12   0     yes      (n/a)              (n/a)
	
	Public nDefaultTariff As Integer
    Public sWait_type As String
    Public nWait_quan As Integer

	Private pclsTar_am_basprod As Tar_am_basprod
	Private pclsTar_am_bas As Tar_am_bas
	
	'**%Update: This method is in charge of updating records in the table "Health".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "Health". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lrecupdHealth_1 As eRemoteDB.Execute
		
		lrecupdHealth_1 = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.updHealth_1'
		'**+Information read on February 7,2000 11:17:46
		'+Definición de parámetros para stored procedure 'insudb.updHealth_1'
		'+Información leída el 7/02/2000 11:17:46
		
		With lrecupdHealth_1
			.StoredProcedure = "updHealth_1"
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_comp", nGroup_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssuedat", dIssuedat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sWait_type", sWait_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWait_quan", nWait_quan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Update = True
            End If
		End With
		'UPGRADE_NOTE: Object lrecupdHealth_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdHealth_1 = Nothing
		
	End Function
	
	'%insPreAM002: realiza la acción inicial de la ventana AM002
	Public Function insPreAM002(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer) As Boolean
		pclsTar_am_bas = New Tar_am_bas
		
		On Error GoTo insPreAM002_Err
		
		pclsTar_am_bas = New Tar_am_bas
		
		'+ Se cargan los valores por defecto pertenecientes a la tarifa
		
		insPreAM002 = insReaTar_am_bas(sCertype, nBranch, nProduct, nPolicy, dEffecdate, nTransaction)
		
insPreAM002_Err: 
		If Err.Number Then
			insPreAM002 = False
		End If
	End Function
	
	'% insReaTar_am_bas : lee las tarifas asociadas a un producto y además devuelve la tarifa que está señalada por defecto
	Private Function insReaTar_am_bas(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer) As Boolean
		Dim lintPos As Integer
		Dim lblnDefault As Boolean
		Dim lintTariff As Integer
		
		lintPos = 0
        lblnDefault = False

        If pclsTar_am_basprod Is Nothing Then
            pclsTar_am_basprod = New Tar_am_basprod
        End If
		'+ Se procede a leer las tarifas asociadas al producto
		
		With pclsTar_am_basprod
			If .Load(nBranch, nProduct, dEffecdate) Then
				'+ Se verifica si alguna tarifa está señalada para el producto y así poderla asignar a la póliza
				
				Do While .Item(lintPos) And Not lblnDefault
					If .sDefaulti = "1" Then
						lblnDefault = True
					Else
						lintPos = lintPos + 1
					End If
				Loop 
			End If
		End With
		
		lblnDefault = False
		lintPos = 0
		
		'+ Se verifica si alguna tarifa está preseleccionada para la póliza previamente
		
		With pclsTar_am_bas
			If .Load(sCertype, nBranch, nProduct, nPolicy, dEffecdate, True) Then
				sDefaulti = .sDefaulti
				Do While .Item(lintPos) And Not lblnDefault
					If .sDefaulti = "1" Then
						lblnDefault = True
					Else
						lintPos = lintPos + 1
					End If
				Loop 
				
				'+ Si la trnasacción es Modificación de certificado (temporal o normal) o  si es la emisión de algún certificado
				'+ se asigna la tarifa asociada en la tabla de datos particulares previamente
				
				If nTransaction = 15 Or nTransaction = 2 Or nTransaction = 14 Then
					Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
					lintTariff = nTariff
					
					'+ De no tener tarifa previa en la tabla de datos particualres, se asocia la de la póliza y sino, la del producto pues no existe ninguna cargada previamente
					
					If lintTariff <> 0 Then
						nDefaultTariff = nTariff
					Else
						If lblnDefault Then
							nDefaultTariff = .nTariff
						Else
							nDefaultTariff = pclsTar_am_basprod.nTariff
						End If
					End If
				Else
					
					'+ De no tratarse de un certificado, se asocia la tarifa de la póliza y sino, la del producto
					
					If lblnDefault Then
						nDefaultTariff = .nTariff
					Else
						nDefaultTariff = pclsTar_am_basprod.nTariff
					End If
				End If
			Else
				
				'+ Si no se ha asociado una tarifa previa a la póliza, se asigna la del producto
				
				nDefaultTariff = pclsTar_am_basprod.nTariff
			End If
			
			If nDefaultTariff <> 0 And nDefaultTariff <> eRemoteDB.Constants.intNull Then
				If pclsTar_am_basprod.FindItem(nDefaultTariff) Then
					sDefaulti = pclsTar_am_basprod.sDefaulti
					nBenef_type = pclsTar_am_basprod.nBenef_type
					nDed_amount = pclsTar_am_basprod.nDed_amount
					nLimit = pclsTar_am_basprod.nLimit
				End If
			End If
		End With
	End Function
	
	'%Find: hace la lectura de la tabla de datos particulares para Atención Médica
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaHealth As eRemoteDB.Execute
		
		lrecreaHealth = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaHealth'
		'Información leída el 07/01/2002 05:53:48 p.m.
		
		With lrecreaHealth
			.StoredProcedure = "reaHealthpkg.reaHealth"
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nProduct = .FieldToClass("nProduct")
				Me.nBranch = .FieldToClass("nBranch")
				Me.sCertype = .FieldToClass("sCertype")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nCertif = .FieldToClass("nCertif")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				nCapital = .FieldToClass("nCapital")
				dExpirdat = .FieldToClass("dExpirdat")
				sClient = .FieldToClass("sClient")
				nGroup_comp = .FieldToClass("nGroup_comp")
				dIssuedat = .FieldToClass("dIssuedat")
				nNullcode = .FieldToClass("nNullcode")
				dNulldate = .FieldToClass("dNulldate")
				nPremium = .FieldToClass("nPremium")
				dStartDate = .FieldToClass("dStartdate")
				nTariff = .FieldToClass("nTariff")
				nUsercode = .FieldToClass("nUsercode")
				nTransactio = .FieldToClass("nTransactio")
				nGroup = .FieldToClass("nGroup")
                nSituation = .FieldToClass("nSituation")
                sWait_type = .FieldToClass("sWait_type")
                nWait_quan = .FieldToClass("nWait_quan")
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaHealth may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaHealth = Nothing
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object pclsTar_am_bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		pclsTar_am_bas = Nothing
		'UPGRADE_NOTE: Object pclsTar_am_basprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		pclsTar_am_basprod = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






