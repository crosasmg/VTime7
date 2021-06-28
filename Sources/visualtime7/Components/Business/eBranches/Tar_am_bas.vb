Option Strict Off
Option Explicit On
Public Class Tar_am_bas
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_am_bas.cls                           $%'
	'% $Author:: Nvaplat37                                  $%'
	'% $Date:: 12/12/03 12:21p                              $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	
	'Column_name                    Type        Computed   Length  Prec  Scale Nullable   TrimTrailingBlanks   FixedLenNullInSource
	'----------------------------- ----------- ---------- -------- ----- ----- --------- -------------------- ----------------------
	Public sCertype As String 'char         no         1                   no              no                    no
	Public nBranch As Integer 'smallint     no         2       5     0     no              (n/a)                 (n/a)
	Public nProduct As Integer 'smallint     no         2       5     0     no              (n/a)                 (n/a)
	Public nPolicy As Double 'int          no         4       10    0     no              (n/a)                 (n/a)
	Public nTariff As Integer 'smallint     no         2       5     0     no              (n/a)                 (n/a)
	Public nGroup As Integer 'smallint     no         2       5     0     no              (n/a)                 (n/a)
	Public nRole As Integer 'smallint     no         2       5     0     no              (n/a)                 (n/a)
	Public dEffecdate As Date 'datetime     no         8                   no              (n/a)                 (n/a)
	Public dNulldate As Date 'datetime     no         8                   yes             (n/a)                 (n/a)
	Public sDefaulti As String 'char         no         1                   yes             no                    yes
	
	Public nUsercode As Integer 'smallint     no         2       5     0     no              (n/a)                 (n/a)
	
	'**-Variables that contain the values for conditioning the inquiry
	'-Variables que almacenaran los valores para condicionar la consulta
	Public Enum eStatusInstance1
		eNew = 0
		eQuery = 1
		eExist = 1
		eUpDate = 2
		eDelete = 3
	End Enum
	
	'**-Variable that contains the status of the record
	'-Variable que contiene el estado del registro
	Public nStatInstanc As Insured_he.eStatusInstance
	
	'**-The defined type, that will be associated to the array that will
	'**-contain the data brought from the table, is declared
	'-Se declara el tipo definido al que se le asociará el arreglo que contendrá los
	'-datos traídos de la tabla
	Private Structure typTar_am_bas
		Dim nStatInstanc As Insured_he.eStatusInstance
		Dim sCertype As String
		Dim nBranch As Integer
		Dim nProduct As Integer
		Dim nPolicy As Integer
		Dim nTariff As Integer
		Dim nGroup As Integer
		Dim nRole As Integer
		Dim dEffecdate As Date
		Dim dNulldate As Date
		Dim sDefaulti As String
	End Structure
	
	Private mudtTar_am_bas() As typTar_am_bas
	
	'**-Variable used to indicate if the array contains elements
	'-Variable utilizada para indicar si el arreglo tiene contenido o no
	Private mblnCharge As Boolean
	
	Public nModulec As Integer
	Public nCover As Integer
	
	'**%Load: This method inquires about the medical attention tariffs of a product
	'%Load: Permite consultar las tarifas de Atención médica de un producto
	Public Function Load(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaTar_am_bas_1 As eRemoteDB.Execute
		Dim lintPos As Integer
		
		On Error GoTo Load_Err
		
		If sCertype <> Me.sCertype Or nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or nPolicy <> Me.nPolicy Or dEffecdate <> Me.dEffecdate Or bFind Then
			
			lrecreaTar_am_bas_1 = New eRemoteDB.Execute
			
			With lrecreaTar_am_bas_1
				.StoredProcedure = "reaTar_am_bas_1"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					ReDim mudtTar_am_bas(50)
					lintPos = 0
					Do While Not .EOF
						mudtTar_am_bas(lintPos).sCertype = sCertype
						mudtTar_am_bas(lintPos).nBranch = nBranch
						mudtTar_am_bas(lintPos).nProduct = nProduct
						mudtTar_am_bas(lintPos).nPolicy = nPolicy
						mudtTar_am_bas(lintPos).nTariff = .FieldToClass("nTariff")
						mudtTar_am_bas(lintPos).nGroup = .FieldToClass("nGroup")
						mudtTar_am_bas(lintPos).nRole = .FieldToClass("nRole")
						mudtTar_am_bas(lintPos).dEffecdate = .FieldToClass("dEffecdate")
						mudtTar_am_bas(lintPos).sDefaulti = .FieldToClass("sDefaulti")
						lintPos = lintPos + 1
						.RNext()
					Loop 
					
					Load = True
					
					ReDim Preserve mudtTar_am_bas(lintPos - 1)
					.RCloseRec()
					
					Me.sCertype = sCertype
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nPolicy = nPolicy
					Me.dEffecdate = dEffecdate
				End If
			End With
		Else
			Load = True
		End If
		mblnCharge = Load
		
Load_Err: 
		If Err.Number Then
			Load = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTar_am_bas_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_am_bas_1 = Nothing
	End Function
	
	'**%ADD: This method is in charge of adding new records to the table "Tar_am_bas".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Tar_am_bas". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lreccreTar_am_bas As eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo Add_Err
		
		lreccreTar_am_bas = New eRemoteDB.Execute
		
		With lreccreTar_am_bas
			.StoredProcedure = "creTar_am_bas"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Add = True
				lintCount = CountItem + 1
				
				ReDim Preserve mudtTar_am_bas(lintCount)
				
				mudtTar_am_bas(lintCount).nStatInstanc = Insured_he.eStatusInstance.eftExist
				mudtTar_am_bas(lintCount).sCertype = sCertype
				mudtTar_am_bas(lintCount).nBranch = nBranch
				mudtTar_am_bas(lintCount).nProduct = nProduct
				mudtTar_am_bas(lintCount).nPolicy = nPolicy
				mudtTar_am_bas(lintCount).nTariff = nTariff
				mudtTar_am_bas(lintCount).nGroup = nGroup
				mudtTar_am_bas(lintCount).nRole = nRole
				mudtTar_am_bas(lintCount).dEffecdate = dEffecdate
				mudtTar_am_bas(lintCount).dNulldate = dNulldate
				mudtTar_am_bas(lintCount).sDefaulti = sDefaulti
				mblnCharge = True
			End If
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreTar_am_bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTar_am_bas = Nothing
	End Function
	
	'**%Update: This method is in charge of updating records in the table "Tar_am_bas".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "Tar_am_bas". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lrecupdTar_am_bas As eRemoteDB.Execute
		Dim lintPos As Integer
		
		On Error GoTo Update_Err
		
		lrecupdTar_am_bas = New eRemoteDB.Execute
		
		With lrecupdTar_am_bas
			.StoredProcedure = "updTar_am_bas"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
				lintPos = Position(nTariff, nGroup, nRole)
				mudtTar_am_bas(lintPos).dNulldate = dNulldate
				mudtTar_am_bas(lintPos).sDefaulti = sDefaulti
			End If
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdTar_am_bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTar_am_bas = Nothing
	End Function
	
	'**%Delete: This method is in charge of Deleting records in the table "Tar_am_bas".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Delete: Este método se encarga de eliminar registros en la tabla "Tar_am_bas". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		Dim lrecdelTar_am_bas As eRemoteDB.Execute
		Dim lintPos As Integer
		
		On Error GoTo Delete_Err
		
		lrecdelTar_am_bas = New eRemoteDB.Execute
		
		With lrecdelTar_am_bas
			.StoredProcedure = "delTar_am_bas"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lintPos = Position(nTariff, nGroup, nRole)
				Do While lintPos < CountItem
					mudtTar_am_bas(lintPos).sCertype = mudtTar_am_bas(lintPos + 1).sCertype
					mudtTar_am_bas(lintPos).nBranch = mudtTar_am_bas(lintPos + 1).nBranch
					mudtTar_am_bas(lintPos).nProduct = mudtTar_am_bas(lintPos + 1).nProduct
					mudtTar_am_bas(lintPos).nPolicy = mudtTar_am_bas(lintPos + 1).nPolicy
					mudtTar_am_bas(lintPos).nTariff = mudtTar_am_bas(lintPos + 1).nTariff
					mudtTar_am_bas(lintPos).nGroup = mudtTar_am_bas(lintPos + 1).nGroup
					mudtTar_am_bas(lintPos).nRole = mudtTar_am_bas(lintPos + 1).nRole
					mudtTar_am_bas(lintPos).dEffecdate = mudtTar_am_bas(lintPos + 1).dEffecdate
					mudtTar_am_bas(lintPos).sDefaulti = mudtTar_am_bas(lintPos + 1).sDefaulti
					mudtTar_am_bas(lintPos).dNulldate = mudtTar_am_bas(lintPos + 1).dNulldate
					mudtTar_am_bas(lintPos).nStatInstanc = mudtTar_am_bas(lintPos + 1).nStatInstanc
					lintPos = lintPos + 1
				Loop 
				If lintPos - 1 < 0 Then
					ReDim Preserve mudtTar_am_bas(0)
					mblnCharge = False
				Else
					ReDim Preserve mudtTar_am_bas(lintPos - 1)
				End If
			End If
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecdelTar_am_bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTar_am_bas = Nothing
	End Function
	
	'**%Item: This method is able to find an element in the array by its position.
	'%Item: Permite encontrar un elemento del arreglo por su posición
	Public Function Item(ByVal nIndex As Integer) As Boolean
		If nIndex <= CountItem Then
			Item = True
			With mudtTar_am_bas(nIndex)
				nStatInstanc = .nStatInstanc
				sCertype = .sCertype
				nBranch = .nBranch
				nProduct = .nProduct
				nPolicy = .nPolicy
				nTariff = .nTariff
				nGroup = .nGroup
				nRole = .nRole
				dEffecdate = .dEffecdate
				dNulldate = .dNulldate
				sDefaulti = .sDefaulti
			End With
		End If
	End Function
	
	'**%FindIntem: This function is able to find an element from the array according to the tariff's code.
	'%FindItem: Permite encontrar un elemento del arreglo de acuerdo al código de la tarifa
	Public Function FindItem(ByVal nTariff As Integer, ByVal nGroup As Integer, ByVal nRole As Integer, Optional ByVal bItem As Boolean = False) As Boolean
		Dim lintPos As Integer
		Dim lblnFind As Boolean
		
		lintPos = 0
		
		Do While lintPos <= CountItem And Not lblnFind
			If mudtTar_am_bas(lintPos).nTariff = nTariff And mudtTar_am_bas(lintPos).nGroup = nGroup And mudtTar_am_bas(lintPos).nRole = nRole Then
				lblnFind = True
				FindItem = IIf(bItem, Item(lintPos), True)
			End If
			lintPos = lintPos + 1
		Loop 
	End Function
	
	'**%Position: This method returns the position where an element in the array is located
	'%Position: Permite devolver la posición en la que se encuentra un elemento del arreglo
	Private Function Position(ByVal nTariff As Integer, ByVal nGroup As Integer, ByVal nRole As Integer) As Integer
		Dim lintPos As Integer
		Dim lblnFind As Boolean
		
		lintPos = 0
		lblnFind = False
		
		Position = -1
		
		Do While lintPos <= CountItem And Not lblnFind
			If mudtTar_am_bas(lintPos).nTariff = nTariff And mudtTar_am_bas(lintPos).nGroup = nGroup And mudtTar_am_bas(lintPos).nRole = nRole Then
				lblnFind = True
				Position = lintPos
			End If
			lintPos = lintPos + 1
		Loop 
	End Function
	
	'***CountItem: Property that indicates the number of elements in an array
	'*CountItem: Propiedad que indica el número de elementos en el arreglo
	Public ReadOnly Property CountItem() As Integer
		Get
			If mblnCharge Then
				CountItem = UBound(mudtTar_am_bas)
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
		dNulldate = dtmNull
		sDefaulti = strNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%FindTarPol: This method inquires about the medical attention tariffs of a product
	'%FindTarPol: Permite consultar las tarifas de Atención médica de un producto
	Public Function FindTarPol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaTar_am_bas_2 As eRemoteDB.Execute
		Dim lintPos As Integer
		Dim lintTop As Integer
		
		On Error GoTo FindTarPol_Err
		
		lrecreaTar_am_bas_2 = New eRemoteDB.Execute
		
		With lrecreaTar_am_bas_2
			.StoredProcedure = "reaTar_am_bas_2"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindTarPol = True
				lintPos = 0
				lintTop = 0
				Do While Not .EOF
					If lintTop = lintPos Then
						lintTop = lintTop + 50
						ReDim mudtTar_am_bas(lintTop)
					End If
					mudtTar_am_bas(lintPos).sCertype = sCertype
					mudtTar_am_bas(lintPos).nBranch = nBranch
					mudtTar_am_bas(lintPos).nProduct = nProduct
					mudtTar_am_bas(lintPos).nPolicy = nPolicy
					mudtTar_am_bas(lintPos).nTariff = .FieldToClass("nTariff", 0)
					mudtTar_am_bas(lintPos).nGroup = .FieldToClass("nGroup", 0)
					mudtTar_am_bas(lintPos).nRole = .FieldToClass("nRole", 0)
					mudtTar_am_bas(lintPos).dEffecdate = .FieldToClass("dEffecdate", "01/01/1800")
					mudtTar_am_bas(lintPos).sDefaulti = .FieldToClass("sDefaulti", "1")
					lintPos = lintPos + 1
					.RNext()
				Loop 
				ReDim Preserve mudtTar_am_bas(lintPos - 1)
				.RCloseRec()
			Else
				FindTarPol = False
			End If
		End With
		
FindTarPol_Err: 
		If Err.Number Then
			FindTarPol = False
		End If
		'UPGRADE_NOTE: Object lrecreaTar_am_bas_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_am_bas_2 = Nothing
	End Function
	
	'%getCountTar_am_basDefaulti: devuelve la fecha de última modificación de la tabla
	Public Function getCountTar_am_basDefaulti(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nTariff As Integer, ByVal nRole As Integer, ByVal nGroup As Integer, ByVal dEffecdate As Date, ByVal sDefaulti As String) As Integer
		Dim lclsExecute As eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo getCountTar_am_basDefaulti_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		With lclsExecute
			.StoredProcedure = "getCountTar_am_basDefaulti"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", lintCount, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			getCountTar_am_basDefaulti = .Parameters("nCount").Value
		End With
		
getCountTar_am_basDefaulti_Err: 
		If Err.Number Then
			getCountTar_am_basDefaulti = -1
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	
	'%getCountTar_am_bas: Devuelve la cantidad de registros marcados por defecto
	Public Function getCountTar_am_bas(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal sDefaulti As String) As Integer
		Dim lclsExecute As eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo getCountTar_am_bas_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		With lclsExecute
			.StoredProcedure = "getCountTar_am_bas"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", lintCount, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			getCountTar_am_bas = .Parameters("nCount").Value
		End With
		
getCountTar_am_bas_Err: 
		If Err.Number Then
			getCountTar_am_bas = -1
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	
	
	'%FindDeftValues: Obtiene los valores asociados a la tarifa a mostrar por defecto.
	Public Function FindDeftValues(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecTar_am_bas As eRemoteDB.Execute
		Dim lintTariff As Integer
		Dim lintGroup As Integer
		Dim lintRole As Integer
		Dim lintModulec As Integer
		Dim lintCover As Integer
		
		On Error GoTo FindDeftValues_Err
		
		If sCertype <> Me.sCertype Or nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or nPolicy <> Me.nPolicy Or dEffecdate <> Me.dEffecdate Or bFind Then
			
			lrecTar_am_bas = New eRemoteDB.Execute
			
			With lrecTar_am_bas
				.StoredProcedure = "getTar_am_bas_defvalue"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTariff_o", lintTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRole_o", lintRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGroup_o", lintGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec_o", lintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover_o", lintCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(False) Then
					If .Parameters("nTariff_o").Value > 0 Then
						Me.nTariff = .Parameters("nTariff_o").Value
						Me.nGroup = .Parameters("nGroup_o").Value
						Me.nRole = .Parameters("nRole_o").Value
						Me.nModulec = .Parameters("nModulec_o").Value
						Me.nCover = .Parameters("nCover_o").Value
						
						FindDeftValues = True
						
						Me.sCertype = sCertype
						Me.nBranch = nBranch
						Me.nProduct = nProduct
						Me.nPolicy = nPolicy
						Me.dEffecdate = dEffecdate
					End If
				End If
			End With
		Else
			FindDeftValues = True
		End If
		
FindDeftValues_Err: 
		If Err.Number Then
			FindDeftValues = False
		End If
		'UPGRADE_NOTE: Object lrecTar_am_bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTar_am_bas = Nothing
	End Function
	
	'%Find_Defaulti: Obtiene los valores asociados a la tarifa a mostrar por defecto.
	Public Function Find_Defaulti(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sDefaulti As String, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecTar_am_bas As eRemoteDB.Execute
		Dim lintTariff As Integer
		Dim lintGroup As Integer
		Dim lintRole As Integer
		Dim lintModulec As Integer
		Dim lintCover As Integer
		
		On Error GoTo Find_Defaulti_Err
		
		If sCertype <> Me.sCertype Or nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or nPolicy <> Me.nPolicy Or dEffecdate <> Me.dEffecdate Or sDefaulti <> Me.sDefaulti Or bFind Then
			
			lrecTar_am_bas = New eRemoteDB.Execute
			
			With lrecTar_am_bas
				.StoredProcedure = "getTar_am_bas_defvalue"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTariff_o", lintTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRole_o", lintRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGroup_o", lintGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec_o", lintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover_o", lintCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run(False) Then
					If .Parameters("nTariff_o").Value > 0 Then
						Me.nTariff = .Parameters("nTariff_o").Value
						Me.nGroup = .Parameters("nGroup_o").Value
						Me.nRole = .Parameters("nRole_o").Value
						Me.nModulec = .Parameters("nModulec_o").Value
						Me.nCover = .Parameters("nCover_o").Value
						
						Find_Defaulti = True
						
						Me.sCertype = sCertype
						Me.nBranch = nBranch
						Me.nProduct = nProduct
						Me.nPolicy = nPolicy
						Me.dEffecdate = dEffecdate
						Me.sDefaulti = sDefaulti
					End If
				End If
			End With
		Else
			Find_Defaulti = True
		End If
		
Find_Defaulti_Err: 
		If Err.Number Then
			Find_Defaulti = False
		End If
		'UPGRADE_NOTE: Object lrecTar_am_bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTar_am_bas = Nothing
	End Function
	
	
	'%insCreUpdTar_am_bas: Permite actualizar la información de la tabla tar_am_bas cuando se está en la secuencia de póliza.
	Public Function insCreUpdTar_am_bas(ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTariff As Integer, ByVal nRole As Integer, ByVal nGroup As Integer, ByVal sDefaulti As String, ByVal nUsercode As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
		Dim lrecTar_am_bas As eRemoteDB.Execute
		
		On Error GoTo insCreUpdTar_am_bas_Err
		
		lrecTar_am_bas = New eRemoteDB.Execute
		
		With lrecTar_am_bas
			.StoredProcedure = "insCreUpdTar_am_bas"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
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
			.Parameters.Add("sDefaulti", IIf(sDefaulti = String.Empty, "2", sDefaulti), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insCreUpdTar_am_bas = .Run(False)
		End With
		
insCreUpdTar_am_bas_Err: 
		If Err.Number Then
			insCreUpdTar_am_bas = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTar_am_bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTar_am_bas = Nothing
	End Function
	
	'%valTar_am_bas: Verifica la existencia de información en la tabla tar_am_bas (maestro de tarifas)
	Public Function valTar_am_bas(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nTariff As Integer, ByVal nRole As Integer, ByVal nGroup As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo valTar_am_bas_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		With lclsExecute
			.StoredProcedure = "valExistsTar_am_bas"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				valTar_am_bas = True
			End If
		End With
		
valTar_am_bas_Err: 
		If Err.Number Then
			valTar_am_bas = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
End Class






