Option Strict Off
Option Explicit On
Public Class Tab_goods
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_goods.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Properties according to the table in the system on November 08,2000.
	'- Propiedades según la tabla en el sistema al 08/11/2000.
	'**- The key fields corresponds to:  sCertype, nBranch, nProduct, nPolicy, nCertif, nId and dEffecdate.
	'- Los campos llave de la tabla corresponden a: sCertype, nBranch, nProduct, nPolicy, nCertif, nId y dEffecdate.
	
	'   Column_name                    Type     Computed     Length  Prec  Scale Nullable   TrimTrailingBlanks    FixedLenNullInSource
	Public nBranch As Integer 'smallint    no          2      5     0     no              (n/a)                  (n/a)
	Public nProduct As Integer 'smallint    no          2      5     0     no              (n/a)                  (n/a)
	Public nCode_good As Integer 'smallint    no          2      5     0     no              (n/a)                  (n/a)
	Public sDescript As String 'char        no         30                  yes             no                     yes
	Public sShort_des As String 'char        no         12                  yes             no                     yes
	Public sStatregt As String 'char        no          1                  yes             no                     yes
	Public nUsercode As Integer 'smallint    no          2      5     0     yes             (n/a)                  (n/a)
	Public nRate As Double 'decimal     no          5      9     6     yes             (n/a)                  (n/a)
	Public sRoutine As String 'char        no         12                  yes             no                     yes
	Public nRatChaAdd As Double 'decimal     no          5      6     2     yes             (n/a)                  (n/a)
	Public nRatChaSub As Double 'decimal     no          5      6     2     yes             (n/a)                  (n/a)
	Public nLevelCha As Integer 'smallint    no          2      5     0     yes             (n/a)                  (n/a)
	Public sChange_typ As String 'char        no          1                  yes             no                     yes
	
	'**- Auxiliary variable
	'- Variables auxiliares
	'**- contains the designer's rate
	'- Contiene la tasa del diseñador
	
	Public nRateProp As Double
	
	'**- Contains the designer's premium
	'- Contiene la prima del diseñador
	
	Public nPrem_prop As Double
	
	'**- Indicate the origin of the change (T: rate, P: premium)
	'- Indica el origen del cambio (T:Tasa, P: Prima)
	
	Public sOrigin As String
	Public sSelected As String
	Public sExist As String
	Public dEffecdate As Date
	
	'**- Indicator that the user can increase the values shown in the system,
	'**- during the treatment of the policy,
	'- Indicador de si el usuario puede aumentar, durante el tratamiento de la póliza,
	'- los valores mostrados por el sistema
	
	Private mstrIncrease As String
	
	'**- Indicator that the the user can decrease the values shown in the system,
	'**- during the treatment of the policy,
	'- Indicador de si el usuario puede disminuir, durante el tratamiento de la póliza,
	'- los valores mostrados por el sistema
	
	Private mstrDecrease As String
	Private mstrAction As String
	
	'**% insLoadTypeGood: Prepares the data of the type of good insured.
	'% insLoadTypeGoods : Prepara los datos del tipo de bien asegurado
	Private Sub insLoadTypeGoods(ByRef lclsTab_goods As Tab_goods)
		
		'**+ Keep the default values of the goods defined in the porduct in the arrengement lvntDataGoods
		'+ Se almacenan en el arreglo lvntDataGoods los valores por defecto de los bienes definidos en el producto
		'**+ Make the call to the execution of the routine that restores the default premium or the rate
		'**+ of the insured good.
		'+ Se hace el llamado a la ejecución de la rutina que devuelve la prima o la  tasa por
		'+ defecto del bien asegurado
		
		If Not (Trim(lclsTab_goods.sRoutine) = String.Empty) Then
			Call insExecRoutine((lclsTab_goods.sRoutine))
		Else
			Me.nRateProp = 0
			Me.nPrem_prop = 0
		End If
		
		'**+ Origin
		'+ Origen
		
		Me.sOrigin = "T"
		
		'**+ (Rate)
		'+ (Tasa)
		
		If Me.nRateProp = 0 Then
			If Not (lclsTab_goods.nRate = eRemoteDB.Constants.intNull) Then
				Me.nRateProp = CDec(lclsTab_goods.nRate)
			End If
		End If
		
		'**+ (Premium)
		'+ (Prima)
		
		If Me.nPrem_prop <> 0 Then
			Me.sOrigin = "P"
		Else
			Me.nPrem_prop = 0
		End If
		
		'**+ Capture the maximum percentage for increase the policy rate
		'+ Se capturan el porcentaje máximos para aumentar la tasa de la póliza
		
		If lclsTab_goods.nRatChaAdd = eRemoteDB.Constants.intNull Then
			lclsTab_goods.nRatChaAdd = 0
		End If
		
		'**+ Capture the minumum percentage to decrease the policy rate.
		'+ Se capturan el porcentaje mínimo para disminuir la tasa de la póliza
		
		If lclsTab_goods.nRatChaSub = eRemoteDB.Constants.intNull Then
			lclsTab_goods.nRatChaSub = 0
		End If
		
		'**+ Capture the type of change allowed to decrease the policy rate.
		'+ Se capturan el tipo de cambio permitido para disminuir la tasa de la póliza
		
		If Trim(lclsTab_goods.sChange_typ) = String.Empty Then
			Me.sChange_typ = CStr(0)
		End If
		
	End Sub
	
	'% insExecRoutine :
    Private Sub insExecRoutine(ByVal lstrRoutine As String, Optional ByVal lintRateprop As Integer = 0)
        Select Case lstrRoutine
            Case "insBasic"
                Me.nPrem_prop = insBasic(lintRateprop)
                Me.nRateProp = lintRateprop
            Case Else
                Me.nPrem_prop = 0
                Me.nRateProp = 0
        End Select

    End Sub
	
	'% insBasic :
    Private Function insBasic(ByVal lintRateprop As Integer) As Integer
        insBasic = 5000 * (lintRateprop / 100)
    End Function
	
	'**% insValRate. Make the correspondent validations on the rate in comparision to the
	'**% designer's product definition.
	'% insValRate : Realiza las validaciones correspondientes sobre la tasa en comparación con la
	'%              definición del diseñador de producto.
	Public Function insValRate(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCode_good As Integer, ByVal nCapital As Double, ByVal nRateProp As Double, ByVal nPremium As Double, ByRef lobjErrors As eFunctions.Errors) As Boolean
		Dim lclsTab_goodses As Tab_goodses
		lclsTab_goodses = New Tab_goodses
		
		Dim lclsTab_goods As Tab_goods
		lclsTab_goods = New Tab_goods


        insValRate = True
		
		If lclsTab_goodses.Find(nBranch, nProduct) Then
			
			For	Each lclsTab_goods In lclsTab_goodses
				If nCode_good = lclsTab_goods.nCode_good Then
					
					'**+ Load the data that corresponds to the rate.
					'+ Cargar los datos que corresponden a la tasa.
					Call insLoadTypeGoods(lclsTab_goods)
					'**+ Rate
					'+ Tasa
					If nRateProp > Me.nRateProp Then
						If lclsTab_goods.sChange_typ <> "2" And lclsTab_goods.sChange_typ <> "4" Then
							Call lobjErrors.ErrorMessage(sCodispl, 3307)
							insValRate = False
						Else
							If lclsTab_goods.nRatChaAdd <> 0 And lclsTab_goods.nRatChaAdd <> eRemoteDB.Constants.intNull Then
								If System.Math.Abs(InsCalVar((Me.nRateProp), nRateProp)) > lclsTab_goods.nRatChaAdd Then
									Call lobjErrors.ErrorMessage(sCodispl, 3833,  , eFunctions.Errors.TextAlign.RigthAling, "(" & lclsTab_goods.nRatChaAdd & ")")
									insValRate = False
								End If
							End If
						End If
					End If
					
					If nRateProp < Me.nRateProp Then
						If lclsTab_goods.sChange_typ <> "3" And lclsTab_goods.sChange_typ <> "4" Then
							Call lobjErrors.ErrorMessage(sCodispl, 3306)
							insValRate = False
						Else
							If lclsTab_goods.nRatChaSub <> 0 And lclsTab_goods.nRatChaSub <> eRemoteDB.Constants.intNull Then
								If System.Math.Abs(InsCalVar((Me.nRateProp), nRateProp)) > lclsTab_goods.nRatChaSub Then
									Call lobjErrors.ErrorMessage(sCodispl, 3834,  , eFunctions.Errors.TextAlign.RigthAling, "(" & lclsTab_goods.nRatChaSub & ")")
									insValRate = False
								End If
							End If
						End If
					End If
					
					'**+ Premium
					'+Prima
                    If nPremium > ((nCapital * nRateProp) / 1000) Then
                        If lclsTab_goods.sChange_typ <> "2" And lclsTab_goods.sChange_typ <> "4" Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3316)
                            insValRate = False
                        Else
                            If lclsTab_goods.nRatChaAdd <> 0 And lclsTab_goods.nRatChaAdd <> eRemoteDB.Constants.intNull Then
                                If System.Math.Abs(InsCalVar((nCapital * nRateProp) / 1000, nPremium)) > lclsTab_goods.nRatChaAdd Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3729, , eFunctions.Errors.TextAlign.RigthAling, "(" & lclsTab_goods.nRatChaAdd & ")")
                                    insValRate = False
                                End If
                            End If
                        End If
                    ElseIf nPremium < ((nCapital * nRateProp) / 1000) Then
                        If lclsTab_goods.sChange_typ <> "3" And lclsTab_goods.sChange_typ <> "4" Then
                            Call lobjErrors.ErrorMessage(sCodispl, 3315)
                            insValRate = False
                        Else
                            If lclsTab_goods.nRatChaSub <> 0 And lclsTab_goods.nRatChaSub <> eRemoteDB.Constants.intNull Then
                                If System.Math.Abs(InsCalVar((nCapital * nRateProp) / 1000, nPremium)) > lclsTab_goods.nRatChaSub Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3730, , eFunctions.Errors.TextAlign.RigthAling, "(" & lclsTab_goods.nRatChaSub & ")")
                                    insValRate = False
                                End If
                            End If
                        End If
                    End If
					
					'**+ When the interest element is found, the search routine is finished.
					'+ Al encontrar el elemento de interés se termina la rutina de busqueda abruptamente.
					Exit For
				End If
			Next lclsTab_goods
		End If
		
		'UPGRADE_NOTE: Object lclsTab_goodses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_goodses = Nothing
		'UPGRADE_NOTE: Object lclsTab_goods may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_goods = Nothing
		
	End Function
	
	'**% insCalVar: Procedure that calculates the variation among two numbers.
	'%insCalVar: Procedimiento que calcula la variacion entre dos numeros.
    Private Function InsCalVar(ByVal nMonto100 As Double, ByVal nMontoX As Double) As Double
        If nMonto100 <> 0 Then
            InsCalVar = (nMontoX * 1000 / nMonto100) - 1000
        Else
            InsCalVar = 0
        End If
    End Function
	
	'**% Delete: Delete an insured good of the Insured Goods table (Tab_goods)
	'% Delete: Elimina un bien asegurado de la tabla de Bienes Asegurados (Tab_goods)
	Public Function Delete() As Boolean
		
		Dim lrecdelTab_goods As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		lrecdelTab_goods = New eRemoteDB.Execute
		
		'**+ Parameter definitiof for stored procedure 'insudb.delTab_goods'
		'+ Definición de parámetros para stored procedure 'insudb.delTab_goods'
		'**+ Information read on May 02,2001  02:53:27 p.m.
		'+ Información leída el 02/05/2001 02:53:27 p.m.
		
		With lrecdelTab_goods
			.StoredProcedure = "delTab_goods"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_good", nCode_good, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecdelTab_goods may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTab_goods = Nothing
		On Error GoTo 0
	End Function
	
	'**% Update: Updates an insured good of the Insured Goods table (Tab_goods)
	'% Update: Actualiza un bien asegurado de la tabla de Bienes Asegurados (Tab_goods)
	Public Function Update() As Boolean
		
		Dim lrecinsTab_goods As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsTab_goods = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.insTab_goods'
		'+ Definición de parámetros para stored procedure 'insudb.insTab_goods'
		'**+ Information read on May 02,2001  03:05:35 p.m.
		'+ Información leída el 02/05/2001 03:05:35 p.m.
		
		With lrecinsTab_goods
			.StoredProcedure = "insTab_goods"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_good", nCode_good, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatChaAdd", nRatChaAdd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatChaSub", nRatChaSub, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevelCha", nLevelCha, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChange_typ", sChange_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsTab_goods may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsTab_goods = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'*** Class_Initialize: controls the creation of each instance of the class
	'* Class_Initialize: Se controla la creación de cada instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		nUsercode = Int(CDbl(GetSetting("TIME", "GLOBALS", "USERCODE", CStr(0))))
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%insValDP100: Validates the page "DP100" as described in the functional specifications
	'%InsValDP100: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "DP100"
	Public Function insValDP100(ByVal sCodispl As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nCode_good As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal nRate As Double = 0, Optional ByVal nRatChaAdd As Double = 0, Optional ByVal nRatChaSub As Double = 0, Optional ByVal nLevelCha As Integer = 0, Optional ByVal sWin_type As String = "", Optional ByVal sAction As String = "") As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsvalfield As eFunctions.valField
		Dim lcolTab_goodses As ePolicy.Tab_goodses
		
		On Error GoTo insValDP100_Err
		
		lclsErrors = New eFunctions.Errors
		lclsvalfield = New eFunctions.valField
		lcolTab_goodses = New ePolicy.Tab_goodses
		
		lclsvalfield.objErr = lclsErrors
		
		If sWin_type <> "PopUp" Then
			If Not lcolTab_goodses.Find(nBranch, nProduct) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1928)
			End If
		End If
		
		If sWin_type = "PopUp" Then
			If sAction = "Add" Then
				If lcolTab_goodses.Find_Dup(nBranch, nProduct, nCode_good) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10004)
				End If
			End If
			
			If (nCode_good = eRemoteDB.Constants.intNull Or nCode_good = 0) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1012,  ,  , "Código")
			End If
			
			If sDescript = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 10005)
			End If
			
			If sShort_des = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 10006)
			End If
			
			If nRate > 0 Then
				lclsvalfield.Min = 0
				lclsvalfield.Max = 100
				lclsvalfield.Descript = "Tasa"
				If Not lclsvalfield.ValNumber(nRate,  , eFunctions.valField.eTypeValField.onlyvalid) Then
					'Call lclsErrors.ErrorMessage(sCodispl, 1935, , LeftAling, "Tasa: [0-100]")
				End If
			End If
			
			If nRatChaAdd > 0 Then
				lclsvalfield.Min = 0
				lclsvalfield.Max = 100
				lclsvalfield.Descript = "% Máx. Aumentar"
				If Not lclsvalfield.ValNumber(nRatChaAdd,  , eFunctions.valField.eTypeValField.onlyvalid) Then
					'Call lclsErrors.ErrorMessage(sCodispl, 1935, , LeftAling, "% Máx. Aumentar: [0-100]")
				End If
			End If
			
			If nRatChaSub > 0 Then
				lclsvalfield.Min = 0
				lclsvalfield.Max = 100
				lclsvalfield.Descript = "% Máx. Disminuir"
				If Not lclsvalfield.ValNumber(nRatChaSub,  , eFunctions.valField.eTypeValField.onlyvalid) Then
					'Call lclsErrors.ErrorMessage(sCodispl, 1935, , LeftAling, "% Máx. Disminuir: [0-100]")
				End If
			End If
			
		End If
		
		insValDP100 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lcolTab_goodses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolTab_goodses = Nothing
		'UPGRADE_NOTE: Object lclsvalfield may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalfield = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValDP100_Err: 
		If Err.Number Then
			insValDP100 = insValDP100 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% insPostDP100: Validate all the introduced data in the specific content zone for "Frame"
	'% insPostDP100: Valida los datos introducidos en la zona de contenido para "frame" especifico
	Public Function insPostDP100(ByVal sAction As String, ByVal nMainAction As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nCode_good As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal nUsercode As Integer = 0, Optional ByVal nRate As Double = 0, Optional ByVal sRoutine As String = "", Optional ByVal nRatChaAdd As Double = 0, Optional ByVal nRatChaSub As Double = 0, Optional ByVal nLevelCha As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal strIncrease As String = "", Optional ByVal strDecrease As String = "") As Boolean
		
		Dim lcolTab_goodses As ePolicy.Tab_goodses
		Dim lclsProd_win As eProduct.Prod_win
		
		lcolTab_goodses = New ePolicy.Tab_goodses
		lclsProd_win = New eProduct.Prod_win
		
		'**+ This assignation is for use the incoming information in all
		'**+ the routines called in insPostDp100, without having to pass it as a parameter.
		'+ Esta asignación es para utilizar la información entrante en todas
		'+ las rutinas llamadas dentro de insPostDP100, sin tener que pasarla como parámetro
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nCode_good = nCode_good
			.sDescript = sDescript
			.sShort_des = sShort_des
			.nUsercode = nUsercode
			.nRate = nRate
			.sRoutine = sRoutine
			.nRatChaAdd = nRatChaAdd
			.nRatChaSub = nRatChaSub
			.nLevelCha = nLevelCha
			.dEffecdate = dEffecdate
			mstrIncrease = strIncrease
			mstrDecrease = strDecrease
			mstrAction = sAction
			
			If nMainAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
				If insCreTab_goods Then
					insPostDP100 = True
					
					If lcolTab_goodses.Find(CShort(.nBranch), CShort(.nProduct)) Then
						Call lclsProd_win.Add_Prod_win(.nBranch, .nProduct, .dEffecdate, "DP100", "2", .nUsercode)
					Else
						Call lclsProd_win.Add_Prod_win(.nBranch, .nProduct, .dEffecdate, "DP100", "1", .nUsercode)
					End If
				End If
			Else
				insPostDP100 = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		'UPGRADE_NOTE: Object lcolTab_goodses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolTab_goodses = Nothing
	End Function
	
	'**% insCreTab_goods: Associate all the selected actions for the user to a transaction
	'% insCreTab_goods: Asocia todas las acciones seleccionadas por el usuario a una transacción
	Private Function insCreTab_goods() As Boolean
		
		Dim lstrChange_typ As String
		
		On Error GoTo insCreTab_goods_Err
		
		insCreTab_goods = True
		
		If (mstrAction = "Add" Or mstrAction = "Update") And Not (Me.nCode_good = eRemoteDB.Constants.intNull) Then
			If mstrIncrease = "1" Then
				lstrChange_typ = IIf(mstrDecrease = "1", "4", "2")
			Else
				lstrChange_typ = IIf(mstrDecrease = "1", "3", "1")
			End If
			Me.sChange_typ = lstrChange_typ
			
			If Not Me.Update Then
				insCreTab_goods = False
			End If
		ElseIf mstrAction = "Del" Then 
			If Not Me.Delete Then
				insCreTab_goods = False
			End If
		End If
		
insCreTab_goods_Err: 
		If Err.Number Then
			insCreTab_goods = False
		End If
		On Error GoTo 0
	End Function
End Class






