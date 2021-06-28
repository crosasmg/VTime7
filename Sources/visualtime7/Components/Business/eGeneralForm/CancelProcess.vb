Option Strict Off
Option Explicit On
Public Class CancelProcess
	'%-------------------------------------------------------%'
	'% $Workfile:: CancelProcess.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 11/08/03 5:32p                               $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'- Código lógico de la transacción que se está trabajando.
	
	Private mstrCodispl As String
	
	'- Variables que contienen lo que se mostrará en los campos Option de la página.
	
	Private mstrFirstDescript As String
	Private mstrSecondDescript As String
	
	'- Variables que indican que campo se mostrará marcado por defecto.
	
	Private mblnFirstValue As Boolean

    Private mblnDisabled As Boolean 
	
	'- Variable para el manejo de eFunctions.
	
	Private mclsValues As eFunctions.Values
	
	'% OptionControls: crea los controles Option a mostrar en la página
	Public Function makeTable(ByVal sCodispl As String) As String
		Dim lstrHTMLCode As String
		
		mclsValues = New eFunctions.Values
		
		mstrCodispl = sCodispl
		
		lstrHTMLCode = "<TABLE WIDTH=""100%"">" & "    <TR>" & "        <TD COLSPAN=""2"">" & OptionControls & "        </TD>" & "    </TR>" & "    <TR>" & "        <TD COLSPAN=""2""><BR></TD>" & "    </TR>" & "    <TR>" & mclsValues.ButtonAcceptCancel( , "opener.top.frames('fraFolder').mblnValid=true;window.close();", True, 2) & "    </TR>" & "</TABLE>"
		makeTable = lstrHTMLCode
	End Function
	
	'% OptionControls: crea los controles Option a mostrar en la página
	Private Function OptionControls() As String
        OptionControls = Me.LoadValues
        OptionControls = OptionControls & mclsValues.OptionControl(0, "optElim", mstrFirstDescript, IIf(mblnFirstValue, "1", "2"), "Delete", ,mblnDisabled) & vbCrLf
		OptionControls = OptionControls & vbCrLf
		OptionControls = OptionControls & mclsValues.OptionControl(0, "optElim", mstrSecondDescript, IIf(Not mblnFirstValue, "1", "2"), "Finish") & vbCrLf
	End Function
	
	'% LoadValues: Busca los datos correspondientes a las opciones a mostrar en la forma.
	Private Function LoadValues() As String
        Dim objContext As New eRemoteDB.ASPSupport
        Dim objLevels As New eSecurity.Secur_sche  

		mblnFirstValue = False
        mblnDisabled   = False 
		mstrFirstDescript = String.Empty
		mstrSecondDescript = String.Empty
		
		LoadValues = "<SCRIPT>"
		
		Select Case mstrCodispl
			
			'+ Secuencia de Clientes
			Case "BC003_K"
				LoadValues = LoadValues & makeSubmit("ClientSeq")
				If getDescript(102, True) Then
					If getDescript(121, True, True) Then
					End If
				End If
				If getDescript(103, False) Then
					If getDescript(104, False, True) Then
					End If
				End If
				
				'+ Secuencia de Cartera
			Case "CA001_K"
				LoadValues = LoadValues & makeSubmit("PolicySeq")
                'Si se trata de un usuario Intermediario, no tiene permitido eliminar cotizaciones/propuestas/polizas
                If objContext.GetASPSessionValue("sTypeUser") = "3" Then
                    mblnDisabled = True
                Else 
                    'Si el nivel de actualizacion del usuario no es 9, no tiene permitido eliminar cotizaciones/propuestas/polizas
                    If objLevels.GetLevelsByTransac(objContext.GetASPSessionValue("sSche_code"),"1","DMECAR") then
                        If objLevels.nAmelevel < 9 then
                            mblnDisabled = True
                        End If
                    End If
                End If

				If getDescript(105, True) Then
				End If
				If getDescript(10500, False) Then
					If getDescript(10501, False, True) Then
					End If
				End If
				
				'+ Secuencia de Intermediarios
			Case "AG001_K"
				LoadValues = LoadValues & makeSubmit("AgentSeq")
				If getDescript(105, True) Then
					If getDescript(122, True, True) Then
					End If
				End If
				If getDescript(106, False) Then
					If getDescript(107, False, True) Then
					End If
				End If
				
				'+ Secuencia de Cobranza
			Case "CO001_K"
				LoadValues = LoadValues & makeSubmit("CollectionSeq")
				If getDescript(105, True) Then
					If getDescript(207, True, True) Then
					End If
				End If
				If getDescript(208, False) Then
					If getDescript(207, False, True) Then
					End If
				End If
				
				'+ Subsecuencia de Cobertura
			Case "DP034_K"
				LoadValues = LoadValues & makeSubmit("CoverSeq")
				If getDescript(105, True) Then
				End If
				If getDescript(106, False) Then
					If getDescript(107, False, True) Then
					End If
				End If
				
				'+ Secuencia de Financiamiento
			Case "FI001_K"
				LoadValues = LoadValues & makeSubmit("FinanceSeq")
				If getDescript(105, True) Then
				End If
				If getDescript(106, False) Then
					If getDescript(107, False, True) Then
					End If
				End If
				
				'+ Secuencia de Transacciones del sistema.
			Case "SG005_k"
				LoadValues = LoadValues & makeSubmit("TransacSeq")
				
				If getDescript(105, True) Then
				End If
				
				If getDescript(106, False) Then
					If getDescript(107, False, True) Then
					End If
				End If
				
				'+ Secuencia del Esquema de seguridad.
			Case "SG013_k"
				LoadValues = LoadValues & makeSubmit("SchemaSeq")
				
				If getDescript(105, True) Then
				End If
				
				If getDescript(106, False) Then
					If getDescript(107, False, True) Then
					End If
				End If
				
				'+ Secuencia de Coberturas genericas de vida.
			Case "DP018G_K"
				LoadValues = LoadValues & makeSubmit("GenCoverSeq")
				
				If getDescript(105, True) Then
				End If
				
				If getDescript(106, False) Then
					If getDescript(107, False, True) Then
					End If
				End If
				
				'+ Subsecuencia de Recargos y descuentos
			Case "DP08B1_K"
				LoadValues = LoadValues & makeSubmit("DiscoExprSeq")
				If getDescript(105, True) Then
				End If
				If getDescript(106, False) Then
					If getDescript(107, False, True) Then
					End If
				End If
				
				'+ Secuencia de Co/Reaseguros
			Case "CR301_k", "CR304_k"
				LoadValues = LoadValues & makeSubmit("CoReinsuran")
				If getDescript(105, True) Then
				End If
				If getDescript(106, False) Then
					If getDescript(107, False, True) Then
					End If
				End If
				
				'+Secuencia de mantenimiento de Convenios de VidActiva
			Case "MVA646_K"
				LoadValues = LoadValues & makeSubmit("MantAgreement_al")
				If getDescript(105, True) Then
				End If
				If getDescript(106, False) Then
					If getDescript(107, False, True) Then
					End If
				End If
				
				'+Secuencia de Asegurados por coberturas
			Case "DP705"
				LoadValues = LoadValues & makeSubmit("RolesSeq")
				If getDescript(105, True) Then
				End If
				If getDescript(106, False) Then
					If getDescript(107, False, True) Then
					End If
				End If
				
		End Select
		
		LoadDescript()
		
		LoadValues = LoadValues & "</SCRIPT>"
	End Function
	
	'% makeSubmit: Cambia la accion de la forma.
	Private Function makeSubmit(ByVal sProject As String) As String
		makeSubmit = "document.forms[0].action = ""/VTimeNet/"
		
		Select Case sProject
			
			'+ Secuencia de Clientes
			Case "ClientSeq"
				makeSubmit = makeSubmit & "Client/ClientSeq/valClientSeq.aspx"
				
				'+ Secuencia de Póliza
			Case "PolicySeq"
				makeSubmit = makeSubmit & "Policy/PolicySeq/valPolicySeq.aspx"
				
				'+ Secuencia de Intermediarios
			Case "AgentSeq"
				makeSubmit = makeSubmit & "Agent/AgentSeq/valAgentSeq.aspx"
				
				'+ Secuencia de Cobranza
			Case "CollectionSeq"
				makeSubmit = makeSubmit & "Collection/CollectionSeq/valCollectionSeq.aspx"
				
				'+ Subsecuencia de Cobertura
			Case "CoverSeq"
				makeSubmit = makeSubmit & "Product/ProductSeq/CoverSeq/valCoverSeq.aspx"
				
				'+ Subsecuencia de Recargos y descuentos
			Case "DiscoExprSeq"
				makeSubmit = makeSubmit & "Product/ProductSeq/DiscoExprSeq/valDiscoExprSeq.aspx"
				
				'+ Secuencia de Financiamiento
			Case "FinanceSeq"
				makeSubmit = makeSubmit & "Finance/FinanceSeq/valFinanceSeq.aspx"
				
				'+ Secuencia de Transacciones del sistema.
				
			Case "TransacSeq"
				makeSubmit = makeSubmit & "Security/Security/valSecuritySeq.aspx"
				
				'+ Secuencia del Esquema de seguridad.
			Case "SchemaSeq"
				makeSubmit = makeSubmit & "Security/Security/valSecuritySeqSchema.aspx"
				
				'+ Secuencia del coberturas genericas de vida.
			Case "GenCoverSeq"
				makeSubmit = makeSubmit & "Product/Product/ValCoverSeq.aspx"
				
				'+ Secuencia de Co/Reaseguros.
			Case "CoReinsuran"
				makeSubmit = makeSubmit & "CoReinsuran/CoReinsuran/valCoReinsuran.aspx"
				
				'+Secuencia de mantenimiento de Convenios de VidActiva
			Case "MantAgreement_al"
				makeSubmit = makeSubmit & "Maintenance/MantAgreement_al/ValAgreementSeq.aspx"
				
				'+Secuencia de asegurados por coberturas
			Case "RolesSeq"
				makeSubmit = makeSubmit & "Product/ProductSeq/RolesSeq/valRolesSeq.aspx"
				
		End Select
		
		makeSubmit = makeSubmit & "?sCodispl=GE101&WindowType=PopUp"";"
	End Function
	
	'% getDescript: toma la descripción de Table563
	Private Function getDescript(ByVal nCode As Integer, ByVal bFirstControl As Boolean, Optional ByVal bConcatMessage As Boolean = False) As Boolean
		
		If nCode > 0 Then
			getDescript = True
			If bFirstControl Then
				If bConcatMessage Then
					mstrFirstDescript = mstrFirstDescript & "|" & CStr(nCode)
				Else
					mstrFirstDescript = CStr(nCode)
				End If
			Else
				If bConcatMessage Then
					mstrSecondDescript = mstrSecondDescript & "|" & CStr(nCode)
				Else
					mstrSecondDescript = CStr(nCode)
				End If
			End If
		Else
			getDescript = True
		End If
	End Function
	
	'% Loaddescript: Transforma la concatenacion de los codigos en descripción
	Private Function LoadDescript() As Object
		
		Dim lrecAux As eRemoteDB.Execute
		
		On Error GoTo Loaddescript_err
		lrecAux = New eRemoteDB.Execute
		
		With lrecAux
			.StoredProcedure = "reaDescript_code"
			.Parameters.Add("mstrFirstdescript", mstrFirstDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("mstrSeconddescript", mstrSecondDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			mstrFirstDescript = .Parameters("mstrFirstDescript").Value
			mstrSecondDescript = .Parameters("mstrSecondDescript").Value
		End With
		
Loaddescript_err: 
		If Err.Number Then
		End If
		'UPGRADE_NOTE: Object lrecAux may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecAux = Nothing
		On Error GoTo 0
	End Function
End Class






