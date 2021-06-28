Option Strict Off
Option Explicit On
Public Class Tar_am_pol
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_am_pol.cls                           $%'
	'% $Author:: Nvaplat37                                  $%'
	'% $Date:: 12/12/03 12:21p                              $%'
	'% $Revision:: 18                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Description of the Tar_am_pol table ' Composition of the tariff group
	'**-of Medical Attention of the policies taken on february 02, 2000.
	'-Descripción de la tabla Tar_am_pol 'Composiciones de grupo de las tarifas de
	'-atención médica de las pólizas
	
	'Column_name                    Type        Computed   Length  Prec  Scale  Nullable   TrimTrailingBlanks   FixedLenNullInSource
	'----------------------------- ----------- ---------- -------- ----- ----- ---------- -------------------- ----------------------
	Public sCertype As String 'char          no      1                       no            yes                    no
	Public nBranch As Integer 'smallint      no      2         5     0       no            (n/a)                  (n/a)
	Public nProduct As Integer 'smallint      no      2         5     0       no            (n/a)                  (n/a)
	Public nPolicy As Double 'int           no      4         10    0       no            (n/a)                  (n/a)
	Public nTariff As Integer 'smallint      no      2         5     0       no            (n/a)                  (n/a)
	Public nGroup As Integer 'smallint      no      2         5     0       no            (n/a)                  (n/a)
	Public nRole As Integer 'smallint      no      2         5     0       no            (n/a)                  (n/a)
	Public dEffecdate As Date 'datetime      no      8                       no            (n/a)                  (n/a)
	Public nAge_End As Integer 'smallint      no      2         5     0       yes           (n/a)                  (n/a)
	Public nAge_init As Integer 'smallint      no      2         5     0       no            (n/a)                  (n/a)
	Public nGroup_comp As Integer 'smallint          no      1                       no            yes                    no
	Public nGroupDed As Double 'smallint          no      1                       no            yes                    no
	Public dNulldate As Date 'datetime      no      8                       yes           (n/a)                  (n/a)
    Public nPremium As Double 'decimal       no      9         10    2       yes           (n/a)                  (n/a)
    Public nCapital As Double
	
	Public nUsercode As Integer 'smallint      no      2         5     0       no            (n/a)                  (n/a)
	
	'**-Variable that conatins the status of the record
	'-Variable que contiene el estado del registro
	Public nStatInstanc As Tar_am_bas.eStatusInstance1
	
	'**-Variables that contain the values for conditioning the inquiry
	'- Variables que almacenaran los valores para condicionar la consulta
	Private mstrCertype As String
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngPolicy As Integer
	Private mintTariff As Integer
	Private mintGroup As Integer
	Private mintRole As Integer
	Private mdtmEffecdate As Date
	
	'**-The defined type, that will be associated to the array that will
	'**-contain the data brought from the table, is declared
	'-Se declara el tipo definido al que se le asociará el arreglo que contendrá los
	'-datos traídos de la tabla
	Private Structure typTar_am_pol
		Dim nStatInstanc As Tar_am_bas.eStatusInstance1
		Dim sCertype As String
		Dim nBranch As Integer
		Dim nProduct As Integer
		Dim nPolicy As Integer
		Dim nModulec As Integer
		Dim nCover As Integer
		Dim nTariff As Integer
		Dim nGroup As Integer
		Dim nRole As Integer
		Dim dEffecdate As Date
		Dim nAge_End As Integer
		Dim nAge_init As Integer
		Dim nGroup_comp As Integer
		Dim dNulldate As Date
		Dim nPremium As Double
        Dim nGroupDed As Double
        Dim nCapital As Double
	End Structure
	
	Private mudtTar_am_pol() As typTar_am_pol
	
	'**-Variable used to indicate if the array contains elements
	'-Variable utilizada para indicar si el arreglo tiene contenido o no
	Private mblnCharge As Boolean
	
	Private mintModulec As Integer
	Private mintCover As Integer
	
	Public nModulec As Integer
	Public nCover As Integer
	
	'**%Load: This method inquires about the composition of groups of a medical attention tariff
	'%Load: Permite consultar las composiciones de grupo de una tarifa de atención médica.
	Public Function Load(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nTariff As Integer, ByVal nGroup As Integer, ByVal nRole As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaTar_am_pol As eRemoteDB.Execute
		Dim lintPos As Integer
		
		On Error GoTo Load_Err
		
		If sCertype <> mstrCertype Or nBranch <> mlngBranch Or nProduct <> mlngProduct Or nPolicy <> mlngPolicy Or nModulec <> mintModulec Or nCover <> mintCover Or dEffecdate <> mdtmEffecdate Or nTariff <> mintTariff Or nGroup <> mintGroup Or nRole <> mintRole Or bFind Then
			
			lrecreaTar_am_pol = New eRemoteDB.Execute
			
			With lrecreaTar_am_pol
				.StoredProcedure = "reaTar_am_pol"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					ReDim mudtTar_am_pol(50)
					lintPos = 0
					Do While Not .EOF
						mudtTar_am_pol(lintPos).nStatInstanc = Insured_he.eStatusInstance.eftExist
						mudtTar_am_pol(lintPos).sCertype = sCertype
						mudtTar_am_pol(lintPos).nBranch = nBranch
						mudtTar_am_pol(lintPos).nProduct = nProduct
						mudtTar_am_pol(lintPos).nPolicy = nPolicy
						mudtTar_am_pol(lintPos).nModulec = nModulec
						mudtTar_am_pol(lintPos).nCover = nCover
						mudtTar_am_pol(lintPos).dEffecdate = .FieldToClass("dEffecdate")
						mudtTar_am_pol(lintPos).nTariff = nTariff
						mudtTar_am_pol(lintPos).nGroup = nGroup
						mudtTar_am_pol(lintPos).nRole = nRole
						mudtTar_am_pol(lintPos).nAge_End = .FieldToClass("nAge_end")
						mudtTar_am_pol(lintPos).nAge_init = .FieldToClass("nAge_init")
						mudtTar_am_pol(lintPos).nGroup_comp = .FieldToClass("nGroup_comp")
						mudtTar_am_pol(lintPos).dNulldate = .FieldToClass("dNulldate")
						mudtTar_am_pol(lintPos).nPremium = .FieldToClass("nPremium")
                        mudtTar_am_pol(lintPos).nGroupDed = .FieldToClass("nGroupDed")
                        mudtTar_am_pol(lintPos).nCapital = .FieldToClass("nCapital")
                        lintPos = lintPos + 1
						.RNext()
					Loop 
					Load = True
					ReDim Preserve mudtTar_am_pol(lintPos - 1)
					.RCloseRec()
				End If
			End With
			
			mstrCertype = sCertype
			mlngBranch = nBranch
			mlngProduct = nProduct
			mlngPolicy = nPolicy
			mintModulec = nModulec
			mintCover = nCover
			mdtmEffecdate = dEffecdate
			mintTariff = nTariff
			mintGroup = nGroup
			mintRole = nRole
		Else
			Load = mblnCharge
		End If
		
Load_Err: 
		If Err.Number Then
			Load = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTar_am_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_am_pol = Nothing
		mblnCharge = Load
	End Function
	
	'**%ADD: This method is in charge of adding new records to the table "tar_am_pol".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "tar_am_pol". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lintCount As Integer
		Dim lreccreTar_am_pol As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		lreccreTar_am_pol = New eRemoteDB.Execute
		
		With lreccreTar_am_pol
			.StoredProcedure = "creTar_am_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_comp", nGroup_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Add = True
				
				lintCount = CountItem + 1
				
				ReDim Preserve mudtTar_am_pol(lintCount)
				
				mudtTar_am_pol(lintCount).nStatInstanc = Insured_he.eStatusInstance.eftExist
				mudtTar_am_pol(lintCount).sCertype = sCertype
				mudtTar_am_pol(lintCount).nBranch = nBranch
				mudtTar_am_pol(lintCount).nProduct = nProduct
				mudtTar_am_pol(lintCount).nPolicy = nPolicy
				mudtTar_am_pol(lintCount).dEffecdate = dEffecdate
				mudtTar_am_pol(lintCount).nTariff = nTariff
				mudtTar_am_pol(lintCount).nGroup = nGroup
				mudtTar_am_pol(lintCount).nRole = nRole
				mudtTar_am_pol(lintCount).nAge_End = nAge_End
				mudtTar_am_pol(lintCount).nAge_init = nAge_init
				mudtTar_am_pol(lintCount).nGroup_comp = nGroup_comp
				mudtTar_am_pol(lintCount).dNulldate = dNulldate
				mudtTar_am_pol(lintCount).nPremium = nPremium
				
				mblnCharge = True
				
			End If
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreTar_am_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTar_am_pol = Nothing
	End Function
	
	'**%Update: This method is in charge of updating records in the table "tar_am_pol".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "tar_am_pol". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lrecupdTar_am_pol As eRemoteDB.Execute
		Dim lintPos As Integer
		
		On Error GoTo Update_Err
		
		lrecupdTar_am_pol = New eRemoteDB.Execute
		
		With lrecupdTar_am_pol
			.StoredProcedure = "updTar_am_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_comp", nGroup_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lintPos = Position(nAge_init, nGroup_comp)
				If lintPos <> -1 Then
					mudtTar_am_pol(lintPos).nStatInstanc = nStatInstanc
					mudtTar_am_pol(lintPos).sCertype = sCertype
					mudtTar_am_pol(lintPos).nBranch = nBranch
					mudtTar_am_pol(lintPos).nProduct = nProduct
					mudtTar_am_pol(lintPos).nPolicy = nPolicy
					mudtTar_am_pol(lintPos).dEffecdate = dEffecdate
					mudtTar_am_pol(lintPos).nTariff = nTariff
					mudtTar_am_pol(lintPos).nGroup = nGroup
					mudtTar_am_pol(lintPos).nRole = nRole
					mudtTar_am_pol(lintPos).nAge_End = nAge_End
					mudtTar_am_pol(lintPos).nAge_init = nAge_init
					mudtTar_am_pol(lintPos).nGroup_comp = nGroup_comp
					mudtTar_am_pol(lintPos).dNulldate = dNulldate
					mudtTar_am_pol(lintPos).nPremium = nPremium
				End If
			End If
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdTar_am_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTar_am_pol = Nothing
	End Function
	
	'**%Delete: This method is in charge of Deleteing records in the table "tar_am_pol".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Delete: Este método se encarga de eliminar registros en la tabla "tar_am_pol". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		Dim lrecdelTar_am_pol As eRemoteDB.Execute
		Dim lintPos As Integer
		
		On Error GoTo Delete_Err
		
		lrecdelTar_am_pol = New eRemoteDB.Execute
		
		With lrecdelTar_am_pol
			.StoredProcedure = "delTar_am_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_comp", nGroup_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lintPos = Position(nAge_init, nGroup_comp)
				Do While lintPos < CountItem And lintPos > -1
					mudtTar_am_pol(lintPos).nStatInstanc = mudtTar_am_pol(lintPos + 1).nStatInstanc
					mudtTar_am_pol(lintPos).sCertype = mudtTar_am_pol(lintPos + 1).sCertype
					mudtTar_am_pol(lintPos).nBranch = mudtTar_am_pol(lintPos + 1).nBranch
					mudtTar_am_pol(lintPos).nProduct = mudtTar_am_pol(lintPos + 1).nProduct
					mudtTar_am_pol(lintPos).nPolicy = mudtTar_am_pol(lintPos + 1).nPolicy
					mudtTar_am_pol(lintPos).dEffecdate = mudtTar_am_pol(lintPos + 1).dEffecdate
					mudtTar_am_pol(lintPos).nTariff = mudtTar_am_pol(lintPos + 1).nTariff
					mudtTar_am_pol(lintPos).nGroup = mudtTar_am_pol(lintPos + 1).nGroup
					mudtTar_am_pol(lintPos).nRole = mudtTar_am_pol(lintPos + 1).nRole
					mudtTar_am_pol(lintPos).nAge_End = mudtTar_am_pol(lintPos + 1).nAge_End
					mudtTar_am_pol(lintPos).nAge_init = mudtTar_am_pol(lintPos + 1).nAge_init
					mudtTar_am_pol(lintPos).nGroup_comp = mudtTar_am_pol(lintPos + 1).nGroup_comp
					mudtTar_am_pol(lintPos).dNulldate = mudtTar_am_pol(lintPos + 1).dNulldate
					mudtTar_am_pol(lintPos).nPremium = mudtTar_am_pol(lintPos + 1).nPremium
					lintPos = lintPos + 1
				Loop 
				If lintPos - 1 < 0 Then
					ReDim Preserve mudtTar_am_pol(0)
					mblnCharge = False
				Else
					ReDim Preserve mudtTar_am_pol(lintPos - 1)
				End If
			End If
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelTar_am_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTar_am_pol = Nothing
	End Function
	
	'**%Item: Allows to find an element from the arrengement by its position
	'%Item: Permite encontrar un elemento del arreglo por su posición
	Public Function Item(ByVal nIndex As Integer) As Boolean
		If nIndex <= CountItem Then
			Item = True
			With mudtTar_am_pol(nIndex)
				nStatInstanc = .nStatInstanc
				sCertype = .sCertype
				nBranch = .nBranch
				nProduct = .nProduct
				nPolicy = .nPolicy
				dEffecdate = .dEffecdate
				nTariff = .nTariff
				nGroup = .nGroup
				nRole = .nRole
				nAge_End = .nAge_End
				nAge_init = .nAge_init
				nGroup_comp = .nGroup_comp
				dNulldate = .dNulldate
				nPremium = .nPremium
                nGroupDed = .nGroupDed
                nCapital = .nCapital
				
			End With
		End If
	End Function
	
	'**%FindIntem: This function is able to find an element from the array according to the tariff's code,
	'**%the initial age and the composition of the group
	'%FindItem: Permite encontrar un elemento del arreglo de acuerdo al código de la tarifa,
	'%la edad inicial y la composición del grupo
	Public Function FindItem(ByVal nAge_init As Integer, Optional ByVal lblnItem As Boolean = False) As Boolean
		Dim lintPos As Integer
		Dim lblnFind As Boolean
		
		lintPos = 0
		
		Do While lintPos <= CountItem And Not lblnFind
			With mudtTar_am_pol(lintPos)
				If .nAge_init = nAge_init Then
					lblnFind = True
					FindItem = IIf(lblnItem, Item(lintPos), True)
				End If
			End With
			lintPos = lintPos + 1
		Loop 
	End Function
	
	'**%Position: This method returns the position where an element in the array is located
	'%Position: Permite devolver la posición en la que se encuentra un elemento del arreglo
	Private Function Position(ByVal nAge_init As Integer, ByVal nGroup_comp As Integer) As Integer
		Dim lintPos As Integer
		Dim lblnFind As Boolean
		
		lintPos = 0
		lblnFind = False
		
		Position = -1
		
		Do While lintPos <= CountItem And Not lblnFind
			With mudtTar_am_pol(lintPos)
				If .nAge_init = nAge_init And .nGroup_comp = nGroup_comp Then
					lblnFind = True
					Position = lintPos
				End If
			End With
			lintPos = lintPos + 1
		Loop 
	End Function
	
	'***CountItem: Property that indicates the number of elements in an array
	'*CountItem: Propiedad que indica el número de elementos en el arreglo
	Public ReadOnly Property CountItem() As Integer
		Get
			If mblnCharge Then
				CountItem = UBound(mudtTar_am_pol)
			Else
				CountItem = -1
			End If
		End Get
	End Property
	
	'**%Class_Initialize: Controls the creation of an instance of the class
	'%Class_Initialize: Controla la creación de una instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sCertype = strNull
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nTariff = eRemoteDB.Constants.intNull
		nGroup = eRemoteDB.Constants.intNull
		nRole = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nAge_End = eRemoteDB.Constants.intNull
		nAge_init = eRemoteDB.Constants.intNull
		nGroup_comp = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nPremium = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		
		nUsercode = eRemoteDB.Constants.intNull
		
		mstrCertype = strNull
		mlngBranch = eRemoteDB.Constants.intNull
		mlngProduct = eRemoteDB.Constants.intNull
		mlngPolicy = eRemoteDB.Constants.intNull
		mintTariff = eRemoteDB.Constants.intNull
		mintGroup = eRemoteDB.Constants.intNull
		mintRole = eRemoteDB.Constants.intNull
		mintModulec = eRemoteDB.Constants.intNull
		mintCover = eRemoteDB.Constants.intNull
		mdtmEffecdate = dtmNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% valRangeAge: Rutina que permite verificar si la edad está incluída dentro de otro rango
	Public Function valRangeAge(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nTariff As Integer, ByVal nGroup_comp As Integer, ByVal nAge_init As Integer, ByVal nAge_End As Integer, ByVal nRole As Integer, ByVal nGroup As Integer, ByVal dEffecdate As Date, Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0) As Boolean
		Dim lrecinsreaRangTariff As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo insRearangtariff_Err
		
		lrecinsreaRangTariff = New eRemoteDB.Execute
		
		With lrecinsreaRangTariff
			.StoredProcedure = "valRangeTar_am_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_comp", nGroup_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				valRangeAge = True
			End If
		End With
		
insRearangtariff_Err: 
		If Err.Number Then
			valRangeAge = False
		End If
		'UPGRADE_NOTE: Object lrecinsreaRangTariff may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsreaRangTariff = Nothing
		On Error GoTo 0
	End Function
	
	'% AddDefaultValue: se crean los conceptos de facturación en base a la tabla general
	Public Function AddDefaultValue(ByVal sCertype As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTariff As Integer, ByVal nGroup As Integer, ByVal nRole As Integer, ByVal sDefaulti As String, ByVal nUsercode As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
		Dim lrecTar_am_pol As eRemoteDB.Execute
		Dim lclsPolicyWin As Object
		
		On Error GoTo AddDefaultValue_Err
		
		lrecTar_am_pol = New eRemoteDB.Execute
		
		With lrecTar_am_pol
			.StoredProcedure = "insCreTar_am_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			AddDefaultValue = .Run(False)
		End With
		
		'+ Actualiza icono de la secuencia de la transacción "AM002" con el estado "con contenido"
		lclsPolicyWin = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy_Win")
		Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "AM002", "2")
		
AddDefaultValue_Err: 
		If Err.Number Then
			AddDefaultValue = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTar_am_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTar_am_pol = Nothing
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
	End Function
	
	'%insCreUpdTar_am_pol: Rutina que permite leer la información de la tabla de datos básicos de
	'%Cobertura en la Tarifa del Ramo de Atención Médica.
    Public Function insCreUpdTar_am_pol(ByVal sAction As String, ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTariff As Integer, ByVal nRole As Integer, ByVal nGroup As Integer, ByVal nAge_init As Integer, ByVal nAge_End As Integer, ByVal nGroup_comp As Integer, ByVal nPremium As Double, ByVal nUsercode As Integer, ByVal nGroupDed As Double, ByVal nModulec As Integer, ByVal nCover As Integer, Optional ByVal nCapital As Double = 0) As Boolean
        Dim lrecTar_am_pol As eRemoteDB.Execute
        On Error GoTo insCreUpdTar_am_pol_Err
        lrecTar_am_pol = New eRemoteDB.Execute

        With lrecTar_am_pol
            .StoredProcedure = "insCreUpdTar_am_pol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            '+ Si se trata de eliminación
            If sAction = "Del" Then
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nIndic", "4", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                '+ Si se trata de una emisión
                If nTransaction = 1 Or nTransaction = 3 Or nTransaction = 4 Or nTransaction = 5 Or nTransaction = 6 Or nTransaction = 7 Or nTransaction = 18 Or nTransaction = 19 Or nTransaction = 30 Or nTransaction = 31 Then
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nIndic", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Else
                    '+ Si se trata de una modificación normal
                    If nTransaction = 12 Or nTransaction = 14 Then
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        .Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nIndic", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Else
                        '+ Si se trata de una modificación temporal
                        If nTransaction = 15 Or nTransaction = 13 Then
                            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nIndic", "3", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        Else
                            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                            .Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nIndic", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        End If
                    End If
                End If
            End If
            .Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_Init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_End", nAge_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_comp", nGroup_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroupDed", nGroupDed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insCreUpdTar_am_pol = .Run(False)
        End With

insCreUpdTar_am_pol_Err:
        If Err.Number Then
            insCreUpdTar_am_pol = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecTar_am_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecTar_am_pol = Nothing
    End Function
	
	'%Find_First: Obtiene el primer elemnto del detalle de las tarifas asociadas a una póliza (tar_am_pol)
	Public Function Find_First(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nTariff As Integer, ByVal nGroup As Integer, ByVal nRole As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecTar_am_pol As eRemoteDB.Execute
		
		On Error GoTo Find_First_Err
		
		If sCertype <> mstrCertype Or nBranch <> mlngBranch Or nProduct <> mlngProduct Or nPolicy <> mlngPolicy Or dEffecdate <> mdtmEffecdate Or nTariff <> mintTariff Or nGroup <> mintGroup Or nRole <> mintRole Or nModulec <> mintModulec Or nCover <> mintCover Or bFind Then
			
			lrecTar_am_pol = New eRemoteDB.Execute
			
			With lrecTar_am_pol
				.StoredProcedure = "reaTar_am_pol"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.sCertype = sCertype
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nPolicy = nPolicy
					Me.dEffecdate = dEffecdate
					Me.nTariff = nTariff
					Me.nGroup = nGroup
					Me.nRole = nRole
					Me.nModulec = nModulec
					Me.nCover = nCover
					Me.nAge_End = .FieldToClass("nAge_end")
					Me.nAge_init = .FieldToClass("nAge_init")
					Me.nGroup_comp = .FieldToClass("nGroup_comp")
					Me.dNulldate = .FieldToClass("dNulldate")
					Me.nPremium = .FieldToClass("nPremium")
					
					Find_First = True
					.RCloseRec()
				End If
			End With
			
			mstrCertype = sCertype
			mlngBranch = nBranch
			mlngProduct = nProduct
			mlngPolicy = nPolicy
			mdtmEffecdate = dEffecdate
			mintTariff = nTariff
			mintGroup = nGroup
			mintRole = nRole
			mintModulec = nModulec
			mintCover = nCover
		Else
			Find_First = mblnCharge
		End If
		
Find_First_Err: 
		If Err.Number Then
			Find_First = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTar_am_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTar_am_pol = Nothing
	End Function
	'%ValExist_Tar_am_pol: Valida la existencia de tarifas asociadas a una póliza (tar_am_pol)
	Public Function ValExist_Tar_am_pol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecTar_am_pol As eRemoteDB.Execute
		Dim lintExists As Integer
		On Error GoTo ValExist_Tar_am_pol_Err
		lrecTar_am_pol = New eRemoteDB.Execute
		
		With lrecTar_am_pol
			.StoredProcedure = "ValExist_Tar_am_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				ValExist_Tar_am_pol = True
			End If
		End With
		
ValExist_Tar_am_pol_Err: 
		If Err.Number Then
			ValExist_Tar_am_pol = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTar_am_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTar_am_pol = Nothing
	End Function
	'%valExistsTar_am_pol: Verifica la existencia de información en la tabla tar_am_bas (detalle de tarifas)
	Public Function valExistsTar_am_pol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nTariff As Integer, ByVal nRole As Integer, ByVal nGroup As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo valExistsTar_am_pol_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		With lclsExecute
			.StoredProcedure = "valExistsTar_am_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				valExistsTar_am_pol = True
			End If
		End With
		
valExistsTar_am_pol_Err: 
		If Err.Number Then
			valExistsTar_am_pol = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
End Class






