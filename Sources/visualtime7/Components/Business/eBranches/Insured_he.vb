Option Strict Off
Option Explicit On
Public Class Insured_he
	'%-------------------------------------------------------%'
	'% $Workfile:: Insured_he.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Definition of the table that contains the insured persons of a policy - "Insured_he" - taken on February 08, 2000
	'-Definición de la tabla de Asegurados de una póliza - "Insured_he" - tomada el 08/02/2000
	
	'Column_name                    Type        Computed   Length  Prec  Scale Nullable   TrimTrailingBlanks   FixedLenNullInSource
	'----------------------------- ----------- ---------- -------- ----- ----- --------- -------------------- ----------------------
	Public sCertype As String 'char         no         1                   no             no                     no
	Public sClient As String 'char         no         14                  no             no                     no
	Public nBranch As Integer 'smallint     no         2       5     0     no             (n/a)                  (n/a)
	Public sCarnet As String 'char         no         14                  yes            no                     yes
	Public nProduct As Integer 'smallint     no         2       5     0     no             (n/a)                  (n/a)
	Public nPolicy As Double 'int          no         4       10    0     no             (n/a)                  (n/a)
	Public nCertif As Double 'int          no         4       10    0     no             (n/a)                  (n/a)
	Public dInpdate As Date 'datetime     no         8                   yes            (n/a)                  (n/a)
	Public dNulldate As Date 'datetime     no         8                   yes            (n/a)                  (n/a)
	Public dEffecdate As Date 'datetime     no         8                   no             (n/a)                  (n/a)
	Public nAnual_sal As Double 'decimal      no         9       12    0     yes            (n/a)                  (n/a)
	Public nGroup_insu As Integer 'smallint     no         2       5     0     no             (n/a)                  (n/a)
	Public nRelation As Integer 'smallint     no         2       5     0     yes            (n/a)                  (n/a)
	Public nAge_limit As Integer 'smallint     no         2       5     0     yes            (n/a)                  (n/a)
	Public nClass_ap As Integer 'smallint     no         2       5     0     yes            (n/a)                  (n/a)
	Public nNullcode As Integer 'smallint     no         2       5     0     yes            (n/a)                  (n/a)
	Public nQuantity As Integer 'smallint     no         2       5     0     yes            (n/a)                  (n/a)
	Private nUsercode As Integer 'smallint     no         2       5     0     no             (n/a)                  (n/a)
	
	'**-Indicator of the action to be done.
	'-Indicador de la acción a realizar
	
	Public sIndicator As String
	
	'**-Variables that will hold values to condition the inquiry
	'-Variables que almacenaran los valores para condicionar la consulta
	
	Private mstrCertype As String
	Private mintBranch As Integer
	Private mintProduct As Integer
	Private mlngPolicy As Integer
	Private mlngCertif As Integer
	Private mintGroup_insu As Integer
	Private mdtmEffecdate As Date
	
	'**-Enumerated type for the status of an instance
	'-Tipo enumerado para el estado de una instancia
	
	Public Enum eStatusInstance
		eftNew = 0
		eftQuery = 1
		eftExist = 1
		eftUpDate = 2
		eftDelete = 3
	End Enum
	
	
	'**-Variable that has the status of the record
	'-Variable que contiene el estado del registro
	
	Public nStatInstanc As eStatusInstance
	
	'**-The defined type that will be associated to the array that will
	'**-contain the data brought from the table is declared
	'-Se declara el tipo definido al que se le asociará el arreglo que contendrá los
	'-datos traídos de la tabla
	
	Private Structure typInsured_he
		Dim nStatInstanc As eStatusInstance
		Dim sCertype As String
		Dim sClient As String
		Dim nBranch As Integer
		Dim sCarnet As String
		Dim nProduct As Integer
		Dim nPolicy As Integer
		Dim nCertif As Integer
		Dim dInpdate As Date
		Dim dNulldate As Date
		Dim dEffecdate As Date
		Dim nAnual_sal As Double
		Dim nGroup_insu As Integer
		Dim nRelation As Integer
		Dim nAge_limit As Integer
		Dim nClass_ap As Integer
		Dim nNullcode As Integer
		Dim nQuantity As Integer
	End Structure
	
	Private mudtInsured_he() As typInsured_he
	
	'**-Variable used to indicate whether the array contains elements
	'-Variable utilizada para indicar si el arreglo tiene contenido o no
	
	Private mblnCharge As Boolean
	
	'**%Load: This method obtains the excluded illnesses for a product or tariff.
	'%Load : Permite consultar las enfermedades excluídas para una tarifa o producto
	Public Function Load(ByVal lstrCertype As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal llngPolicy As Integer, ByVal llngCertif As Integer, ByVal lintGroup_insu As Integer, ByVal ldtmEffecdate As Date, Optional ByRef lblnFind As Boolean = False) As Boolean
		Dim lrecreaInsured_he_6 As eRemoteDB.Execute
		Dim lintPos As Integer
		
		If lstrCertype <> mstrCertype Or lintBranch <> mintBranch Or lintProduct <> mintProduct Or llngPolicy <> mlngPolicy Or llngCertif <> mlngCertif Or lintGroup_insu <> mintGroup_insu Or ldtmEffecdate <> mdtmEffecdate Or lblnFind Then
			
			lrecreaInsured_he_6 = New eRemoteDB.Execute
			
			'**+Parameter definition for the stored procedure 'insudb.reaInsured_he_6'
			'**+Information read on February 11,2000 14:50:10
			'+Definición de parámetros para stored procedure 'insudb.reaInsured_he_6'
			'+Información leída el 11/02/2000 14:50:10
			
			With lrecreaInsured_he_6
				.StoredProcedure = "reaInsured_he_6"
				.Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGroup_insu", lintGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					ReDim mudtInsured_he(50)
					lintPos = 0
					Do While Not .EOF
						mudtInsured_he(lintPos).nStatInstanc = eStatusInstance.eftExist
						mudtInsured_he(lintPos).sCertype = lstrCertype
						mudtInsured_he(lintPos).sClient = .FieldToClass("sClient", strNull)
						mudtInsured_he(lintPos).nBranch = lintBranch
						mudtInsured_he(lintPos).sCarnet = .FieldToClass("sCarnet", strNull)
						mudtInsured_he(lintPos).nProduct = lintProduct
						mudtInsured_he(lintPos).nPolicy = llngPolicy
						mudtInsured_he(lintPos).nCertif = llngCertif
						mudtInsured_he(lintPos).dInpdate = .FieldToClass("dInpdate", dtmNull)
						mudtInsured_he(lintPos).dNulldate = .FieldToClass("dNulldate", dtmNull)
						mudtInsured_he(lintPos).dEffecdate = .FieldToClass("dEffecdate", dtmNull)
						mudtInsured_he(lintPos).nAnual_sal = .FieldToClass("nAnual_sal", dblNull)
						mudtInsured_he(lintPos).nGroup_insu = lintGroup_insu
						mudtInsured_he(lintPos).nRelation = .FieldToClass("nRelation", eRemoteDB.Constants.intNull)
						mudtInsured_he(lintPos).nAge_limit = .FieldToClass("nAge_limit", eRemoteDB.Constants.intNull)
						mudtInsured_he(lintPos).nClass_ap = .FieldToClass("nClass_ap", eRemoteDB.Constants.intNull)
						mudtInsured_he(lintPos).nNullcode = .FieldToClass("nNullcode", eRemoteDB.Constants.intNull)
						mudtInsured_he(lintPos).nQuantity = .FieldToClass("nQuantity", eRemoteDB.Constants.intNull)
						lintPos = lintPos + 1
						.RNext()
					Loop 
					
					Load = True
					
					ReDim Preserve mudtInsured_he(lintPos - 1)
					.RCloseRec()
				End If
			End With
			
			mstrCertype = lstrCertype
			mintBranch = lintBranch
			mintProduct = lintProduct
			mlngPolicy = llngPolicy
			mlngCertif = llngCertif
			mintGroup_insu = lintGroup_insu
			mdtmEffecdate = ldtmEffecdate
			
			'UPGRADE_NOTE: Object lrecreaInsured_he_6 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaInsured_he_6 = Nothing
		Else
			Load = mblnCharge
		End If
		
		mblnCharge = Load
	End Function
	
	'**%ADD: This method is in charge of adding new records to the table "Insured_he".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Insured_he". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lreccreInsured_he As eRemoteDB.Execute
		Dim lintCount As Integer
		
		lreccreInsured_he = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.creInsured_he'
		'**+information read on February 08, 2000 8:54:34
		'+Definición de parámetros para stored procedure 'insudb.creInsured_he'
		'+Información leída el 8/02/2000 8:54:34
		
		With lreccreInsured_he
			.StoredProcedure = "creInsured_he"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelation", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_limit", nAge_limit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnual_sal", nAnual_sal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCarnet", sCarnet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClass_ap", nClass_ap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuantity", nQuantity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Add = True
				lintCount = CountItem + 1
				
				ReDim Preserve mudtInsured_he(lintCount)
				
				mudtInsured_he(lintCount).nStatInstanc = eStatusInstance.eftExist
				mudtInsured_he(lintCount).sCertype = sCertype
				mudtInsured_he(lintCount).sClient = sClient
				mudtInsured_he(lintCount).nBranch = nBranch
				mudtInsured_he(lintCount).sCarnet = sCarnet
				mudtInsured_he(lintCount).nProduct = nProduct
				mudtInsured_he(lintCount).nPolicy = nPolicy
				mudtInsured_he(lintCount).nCertif = nCertif
				mudtInsured_he(lintCount).dInpdate = dInpdate
				mudtInsured_he(lintCount).dNulldate = dNulldate
				mudtInsured_he(lintCount).dEffecdate = dEffecdate
				mudtInsured_he(lintCount).nAnual_sal = nAnual_sal
				mudtInsured_he(lintCount).nGroup_insu = nGroup_insu
				mudtInsured_he(lintCount).nRelation = nRelation
				mudtInsured_he(lintCount).nAge_limit = nAge_limit
				mudtInsured_he(lintCount).nClass_ap = nClass_ap
				mudtInsured_he(lintCount).nNullcode = nNullcode
				mudtInsured_he(lintCount).nQuantity = nQuantity
				
				mblnCharge = True
			End If
		End With
		'UPGRADE_NOTE: Object lreccreInsured_he may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreInsured_he = Nothing
		
	End Function
	
	'**%Update: This method is in charge of updating records in the table "Insured_he".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "Insured_he". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		
		Dim lrecupdInsured_he As eRemoteDB.Execute
		Dim lintPos As Integer
		
		lrecupdInsured_he = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.updInsured_he'
		'**+Information read on February 08,2000 16:45:53
		'+Definición de parámetros para stored procedure 'insudb.updInsured_he'
		'+Información leída el 8/02/2000 16:45:53
		
		With lrecupdInsured_he
			.StoredProcedure = "updInsured_he"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelation", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClass_ap", nClass_ap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_limit", nAge_limit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnual_sal", nAnual_sal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCarnet", sCarnet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuantity", nQuantity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndicator", sIndicator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
				lintPos = Position(sClient)
				mudtInsured_he(lintPos).nStatInstanc = nStatInstanc
				mudtInsured_he(lintPos).sCertype = sCertype
				mudtInsured_he(lintPos).sClient = sClient
				mudtInsured_he(lintPos).nBranch = nBranch
				mudtInsured_he(lintPos).sCarnet = sCarnet
				mudtInsured_he(lintPos).nProduct = nProduct
				mudtInsured_he(lintPos).nPolicy = nPolicy
				mudtInsured_he(lintPos).nCertif = nCertif
				mudtInsured_he(lintPos).dInpdate = dInpdate
				mudtInsured_he(lintPos).dNulldate = dNulldate
				mudtInsured_he(lintPos).dEffecdate = dEffecdate
				mudtInsured_he(lintPos).nAnual_sal = nAnual_sal
				mudtInsured_he(lintPos).nGroup_insu = nGroup_insu
				mudtInsured_he(lintPos).nRelation = nRelation
				mudtInsured_he(lintPos).nAge_limit = nAge_limit
				mudtInsured_he(lintPos).nClass_ap = nClass_ap
				mudtInsured_he(lintPos).nNullcode = nNullcode
				mudtInsured_he(lintPos).nQuantity = nQuantity
			End If
		End With
		'UPGRADE_NOTE: Object lrecupdInsured_he may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdInsured_he = Nothing
	End Function
	
	'**%Delete: This method is in charge of Deleting records in the table "Insured_he_2".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Delete: Este método se encarga de eliminar registros en la tabla "Insured_he_2". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		Dim lrecdelInsured_he As eRemoteDB.Execute
		Dim lintPos As Integer
		
		lrecdelInsured_he = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.delInsudb_he'
		'**+Information read on February 8,2000 13:17:45
		'+Definición de parámetros para stored procedure 'insudb.delInsured_he'
		'+Información leída el 8/02/2000 13:17:45
		
		With lrecdelInsured_he
			.StoredProcedure = "delInsured_he_2"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndicator", sIndicator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Delete = True
				lintPos = Position(sClient)
				Do While lintPos < CountItem
					mudtInsured_he(lintPos).nStatInstanc = mudtInsured_he(lintPos + 1).nStatInstanc
					mudtInsured_he(lintPos).sCertype = mudtInsured_he(lintPos + 1).sCertype
					mudtInsured_he(lintPos).sClient = mudtInsured_he(lintPos + 1).sClient
					mudtInsured_he(lintPos).nBranch = mudtInsured_he(lintPos + 1).nBranch
					mudtInsured_he(lintPos).sCarnet = mudtInsured_he(lintPos + 1).sCarnet
					mudtInsured_he(lintPos).nProduct = mudtInsured_he(lintPos + 1).nProduct
					mudtInsured_he(lintPos).nPolicy = mudtInsured_he(lintPos + 1).nPolicy
					mudtInsured_he(lintPos).nCertif = mudtInsured_he(lintPos + 1).nCertif
					mudtInsured_he(lintPos).dInpdate = mudtInsured_he(lintPos + 1).dInpdate
					mudtInsured_he(lintPos).dNulldate = mudtInsured_he(lintPos + 1).dNulldate
					mudtInsured_he(lintPos).dEffecdate = mudtInsured_he(lintPos + 1).dEffecdate
					mudtInsured_he(lintPos).nAnual_sal = mudtInsured_he(lintPos + 1).nAnual_sal
					mudtInsured_he(lintPos).nGroup_insu = mudtInsured_he(lintPos + 1).nGroup_insu
					mudtInsured_he(lintPos).nRelation = mudtInsured_he(lintPos + 1).nRelation
					mudtInsured_he(lintPos).nAge_limit = mudtInsured_he(lintPos + 1).nAge_limit
					mudtInsured_he(lintPos).nClass_ap = mudtInsured_he(lintPos + 1).nClass_ap
					mudtInsured_he(lintPos).nNullcode = mudtInsured_he(lintPos + 1).nNullcode
					mudtInsured_he(lintPos).nQuantity = mudtInsured_he(lintPos + 1).nQuantity
					lintPos = lintPos + 1
				Loop 
				If lintPos - 1 < 0 Then
					ReDim Preserve mudtInsured_he(0)
					mblnCharge = False
				Else
					ReDim Preserve mudtInsured_he(lintPos - 1)
				End If
			End If
		End With
		'UPGRADE_NOTE: Object lrecdelInsured_he may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelInsured_he = Nothing
	End Function
	
	'**%Item: This function is able to find an element from the array by its position.
	'%Item: Permite encontrar un elemento del arreglo por su posición
	Public Function Item(ByRef lintindex As Integer) As Boolean
		If lintindex <= CountItem Then
			Item = True
			With mudtInsured_he(lintindex)
				nStatInstanc = nStatInstanc
				sCertype = .sCertype
				sClient = .sClient
				nBranch = .nBranch
				sCarnet = .sCarnet
				nProduct = .nProduct
				nPolicy = .nPolicy
				nCertif = .nCertif
				dInpdate = .dInpdate
				dNulldate = .dNulldate
				dEffecdate = .dEffecdate
				nAnual_sal = .nAnual_sal
				nGroup_insu = .nGroup_insu
				nRelation = .nRelation
				nAge_limit = .nAge_limit
				nClass_ap = .nClass_ap
				nNullcode = .nNullcode
				nQuantity = .nQuantity
			End With
		End If
	End Function
	
	'**%FindIntem: This function is able to find an element from the array according to the client's code.
	'%FindItem : Permite encontrar un elemento del arreglo de acuerdo al código del cliente
	Public Function FindItem(ByRef lstrClient As String, Optional ByRef lblnItem As Boolean = False) As Boolean
		Dim lintPos As Integer
		Dim lblnFind As Boolean
		
		lintPos = 0
		
		Do While lintPos <= CountItem And Not lblnFind
			If mudtInsured_he(lintPos).sClient = lstrClient Then
				lblnFind = True
				FindItem = IIf(lblnItem, Item(lintPos), True)
			End If
			lintPos = lintPos + 1
		Loop 
	End Function
	
	'**%Position: This method returns the position where an element from the array is located
	'%Position: Permite devolver la posición en la que se encuentra un elemento del arreglo
	Private Function Position(ByRef lstrClient As String) As Integer
		Dim lintPos As Integer
		Dim lblnFind As Boolean
		
		lintPos = 0
		lblnFind = False
		
		Position = -1
		
		Do While lintPos <= CountItem And Not lblnFind
			If mudtInsured_he(lintPos).sClient = lstrClient Then
				lblnFind = True
				Position = lintPos
			End If
			lintPos = lintPos + 1
		Loop 
	End Function
	
	'***CountItem: Property that indicates the number of elements in an array
	'*CountItem: Propiedad que indica el número de elementos en el arreglo .
	Public ReadOnly Property CountItem() As Integer
		Get
			If mblnCharge Then
				CountItem = UBound(mudtInsured_he)
			Else
				CountItem = -1
			End If
		End Get
	End Property
End Class






