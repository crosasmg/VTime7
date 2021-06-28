<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eRemoteDB" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eReports" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">

'+ Retarda el refresco de la pagina 
Dim mblnTimeOut As Boolean
Dim llngSize As Object
Dim sKey_Col686 As Object

Dim mobjValues As eFunctions.Values
Dim mobjValCollectionRep As eCollection.ValCollectionRep

Dim sCodispl As String

' +Declaración de las variables que reciben los valores de los campos que se deben validar.
Dim nUserCode As Integer
Dim nCertif As Double
Dim nBranch As Integer
Dim nProduct As Integer
Dim sClient As String
Dim nClaim As Integer
Dim nMovType As Integer
Dim nPolicy As Double
Dim dEndDate As Date
Dim dIniDate As Date

'- Variable auxiliar para pase de valores del encabezado al folder
Dim mstrString As String

'- Objeto para localización de archivos

Dim mstrPath As String
Dim mstrKey As String
Dim mstrProcess As String
Dim mstrNoProcess As String

'- Objeto para el manejo de busqueda de archivos  

Dim mobjFileSystemObject As Object
Dim mobjFile As Object
Dim lstrFileName As String

'- Objeto para el manejo de Reporte

Dim mobjDocuments As eReports.Report
Dim mintCount As Integer
Dim mobjUploadRequest As Object

'- Variable para el manejo de Errores

Dim mstrErrors As Object

'- Variables para el recorrido del grid
Dim lintCountCOL502 As Integer
Dim lintCount As Byte

'- Variables para COL723
Dim lintcbeBranch As Object
Dim lintchkReuse As Byte

'- Variables para COL585 COL704
Dim mstrtProcDate As String
Dim mstrvalBank As String
Dim mstrFileName As String
Dim mstrFileName1 As String
Dim mstrcbeWayPay As String
Dim mstrAgreeApvSef As String
Dim mstrErrorsUpload As String = String.Empty


'- Se define la constante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'% insValCollectionRep: Se realizan las validaciones de las formas
'--------------------------------------------------------------------------------------------
Function insValCollectionRep() As Object
	'--------------------------------------------------------------------------------------------
	
	Dim lstrError As String=String.Empty
	Dim lclsCollectionRepErr As Object
	Dim lclsCollectionRep As Object
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ COL001: Operaciones de cobranza
		Case "COL001"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL001("COL001", mobjValues.StringToDate(.Form.Item("tcdInitDate")), mobjValues.StringToDate(.Form.Item("tcdEndDate")), mobjValues.StringToType(.Form.Item("hddnRecOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeMovType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeInfoOrder"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ COL002: Operaciones de cobranza
		Case "COL002"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL002("COL002", .Form.Item("tcdProcessDate"), .Form.Item("optTypBank"), .Form.Item("cbeCardType"), .Form.Item("chkDef"))
				End If
			End With
			
			'+ Reporte de recibos pendientes.		
		Case "COL003"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				insValCollectionRep = lclsCollectionRep.insValCOL003("COL003", mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeZone"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgentCode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valSupCode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeWayPay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeNStatus_Pre"), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			
			'+ COL005: Reporte de cuadre de cobranzas.				
		Case "COL005"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				insValCollectionRep = lclsCollectionRep.insValCOL005("COL005", mobjValues.StringToType(.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+ COL007: Reporte de control de cheques diferidos.
		Case "COL007"
			lclsCollectionRep = New eCollection.Premium
			With Request
				insValCollectionRep = lclsCollectionRep.insValCOL007("COL007", mobjValues.StringToType(.Form.Item("valOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valAgentCode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdPendDate"), eFunctions.Values.eTypeData.etdDate, True))
			End With
			
			'+ COL009: Reporte de Anulación Automatica.
		Case "COL009"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL009("COL009", mobjValues.StringToType(.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
			'+ COL011: Reporte de desglose de Recibos Cobrados.
		Case "COL011"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL011("COL011", mobjValues.StringToType(.Form.Item("tcdinitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdendDate"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
			'+ COL500: Generación automática de cobranzas.
		Case "COL500"
			With Request
				lclsCollectionRep = New eCollection.CollectionRep
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lclsCollectionRep = New eCollection.CollectionRep
					insValCollectionRep = lclsCollectionRep.insValCOL500("COL500", mobjValues.StringToType(.Form.Item("tcdExpirDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optCurrency"), mobjValues.StringToType(.Form.Item("tcdIncrease"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeTyp_CreCard"), eFunctions.Values.eTypeData.etdLong, True))
				End If
			End With
			
			'+ COL502: Imputación de PAC/TRANSBANK.
		Case "COL502"
                lclsCollectionRep = New eCollection.CollectionRep
                Dim lintCount As Integer = 0
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrString = "&nInsur_Area=" & .Form.Item("cbeInsur_area") & "&nWay_Pay=" & .Form.Item("cbeWay_pay") & "&dLimit_pay=" & .Form.Item("tcdLimit_pay") & "&dPayDate=" & .Form.Item("tcdPayDate")
					insValCollectionRep = lclsCollectionRep.insValCOL502_K("COL502", mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdLimit_pay"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdPayDate"), eFunctions.Values.eTypeData.etdDate, True), Session("nUsercode"))
				Else
                        If .QueryString.Item("WindowType") <> "PopUp" Then
                            If Not IsNothing(.Form.GetValues("Sel")) Then
                                lintCount = .Form.Item("Sel").Split(",").Length
                            End If
                            insValCollectionRep = lclsCollectionRep.insValCOL502Upd("COL502", lintCount)
                        Else
                            insValCollectionRep = vbNullString
                            lintCount = 0
                            mstrString = "&nInsur_Area=" & Request.QueryString.Item("nInsur_area") & "&nWay_Pay=" & Request.QueryString.Item("nWay_Pay") & "&dLimit_pay=" & Request.QueryString.Item("dLimit_pay") & "&dPayDate=" & Request.QueryString.Item("dPayDate")
						
                            insValCollectionRep = lclsCollectionRep.insValCOL502("COL502", mobjValues.StringToType(Request.QueryString.Item("nCount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nWay_Pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("dLimit_pay"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("tctnBank"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCommiss"), eFunctions.Values.eTypeData.etdDouble, True))
                        End If
				End If
			End With
			
			'+ COL507: Reporte imputacion de pagos (ventanilla).
		Case "COL507"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					If Not insUpLoadFile(mstrPath) Then
						lstrError = "1977"
					End If
					insValCollectionRep = lclsCollectionRep.insValCOL507("COL507", _
                                                                         mobjValues.StringToType(.Form("valBank"), eFunctions.Values.eTypeData.etdDouble, True), _
                                                                         mobjValues.StringToType(.Form("valAcc_number"), eFunctions.Values.eTypeData.etdDouble, True), _
                                                                         mobjValues.StringToType(.Form("tcdPayDate"), eFunctions.Values.eTypeData.etdDate, True), _
                                                                         mobjValues.StringToType(.Form("tcdLimit_pay"), eFunctions.Values.eTypeData.etdDate, True), _
                                                                         mstrPath & lstrFileName, _
                                                                         mobjValues.StringToType(.Form("tcnAmountPay"), eFunctions.Values.eTypeData.etdDouble, True), _
                                                                         mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
					
				End If
			End With
			
			'+ COL511: Lecturas de Universos Bancarios.
		Case "COL511"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lstrError = "0"
					If Not insUpLoadFile(mstrPath) Then
						lstrError = "1977"
					End If
					insValCollectionRep = lclsCollectionRep.insValCOL511("COL511", mobjValues.StringToType(mobjUploadRequest.Item("valBank").Item("Value"), eFunctions.Values.eTypeData.etdDouble, True), mobjUploadRequest.Item("tctName").Item("FileName"), mobjValues.StringToType(lstrError, eFunctions.Values.eTypeData.etdDouble, True))
				End If
			End With
			
			'+ COL556: Conciliación automática de primas recaudadas.
		Case "COL556"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL556("COL556", mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("optProcesstyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdOperdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
					
				End If
			End With
			
			'+ COL585: Proceso de pareo de mandatos. 
		Case "COL585"
			
			lclsCollectionRep = New eCollection.CollectionRep
			Call insUpLoadFile(mstrPath) 
            insValCollectionRep = mstrErrorsUpload
			
			'+ COL594: Reimpresión y anulación de boletines. 		
		Case "COL594"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL594("COL594", mobjValues.StringToType(.Form.Item("optOper"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdCollDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcnBullStart"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnBullEnd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCancel_Cod"), eFunctions.Values.eTypeData.etdDouble, True))
					
				End If
				
			End With
			
			'+ COL626: Reporte de recibos para un convenio.
		Case "COL626"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL626("COL626", mobjValues.StringToType(Request.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
			'+ COL628: Cierre de facturación.  
		Case "COL628"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL628("COL628", mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdLastClosed"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
			'+ COL636: Pago de comisión de cobradores.
		Case "COL636"
			lclsCollectionRep = New eCollection.Premium_mo
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL636("COL636", mobjValues.StringToType(.Form.Item("cbeInsurArea"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCollectorType"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ COL684: Traspaso de cartera de cobradores.
		Case "COL684"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL684("COL684", mobjValues.StringToType(.Form.Item("cbeInsurArea"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCollectorPre"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCollectorNew"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
			'+ COL686: Preparación de Ctas. Ctes. de cobradores.
		Case "COL686"
			lclsCollectionRep = New eCollection.Premium_mo
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL686("COL686", mobjValues.StringToType(.Form.Item("cbeInsurArea"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdFinalDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCollectorType"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ COL704: Imputación automática de rechazos.
		Case "COL704"
			lclsCollectionRep = New eCollection.CollectionRep
			'insValCollectionRep = insUpLoadFile(mstrPath)
			
			insValCollectionRep = lclsCollectionRep.insValCOL704("COL704", mobjValues.StringToType(Request.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdPayDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeWayPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valBank"), eFunctions.Values.eTypeData.etdDouble), "", mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble))
			
			'+ COL723: Impresión de mandatos y sus polizas.
		Case "COL723"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL723("COL723", mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
			'+ COL742: Imputación automática de descuento por planilla.
		Case "COL742"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL742("COL742", mobjValues.StringToType(.Form.Item("cbeInsurArea"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdCollectDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ COL777: Imputación automática de descuento por planilla.
		Case "COL777"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL777("COL777", mobjValues.StringToType(.Form.Item("tcdCollectIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdCollectEnd"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
			'+ COL832: Inventario primeras primas.
		Case "COL832"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL832("COL832", mobjValues.StringToType(.Form.Item("cbeIniMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIniYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePerMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPerYear"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
		Case "COL910"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL910("COL910", mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdCollectDateEnd"), eFunctions.Values.eTypeData.etdDate))
					
				End If
			End With
			
		Case "COL911"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValCollectionRep = lclsCollectionRep.insValCOL910("COL911", mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdCollectDateEnd"), eFunctions.Values.eTypeData.etdDate))
					
				End If
			End With
			
			'+ Reporte de pólizas rechadas.		
		Case "COL836"
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				insValCollectionRep = lclsCollectionRep.insValCOL836("COL836", mobjValues.StringToType(.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ VT00067 HAD080 Reporte de Operaciones de Cobranza
		Case "COL01166"
			mobjValCollectionRep = New eCollection.ValCollectionRep
			'insValCollectionRep = mobjValCollectionRep.insValCOL01166(sCodispl, nBranch, dIniDate, dEndDate)
            insValCollectionRep = VbNullString
			mobjValCollectionRep = Nothing
			
		Case Else
			insValCollectionRep = "insValCollectionRep: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
			
	End Select
	
	lclsCollectionRep = Nothing
	
End Function

'% insPostCollectionRep: Se realizan las actualizaciones a las tablas.
'--------------------------------------------------------------------------------------------
Function insPostCollectionRep() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lclsCollectionRep As Object
	Dim lstrbank As Object
	Dim lclsQuery As eRemoteDB.Query
	Dim lclsGeneralFunction As eGeneral.GeneralFunction
	Dim lclsBatch_param As eSchedule.Batch_param
	
	lblnPost = False
	
	Dim lclsColRepCOL832 As eCollection.CollectionRep
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ COL001: Operaciones de cobranzas.
		Case "COL001"
			lblnPost = True
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Call insPrintCollectionRep("COL001")
				End If
			End With
			
			'+ COL003 Reporte de recibos pendientes.   
		Case "COL003"
			lblnPost = True
			Call insPrintCollectionRep("COL003")
			
			'+ COL005 Reporte de cuadre de cobranzas.    
		Case "COL005"
			lblnPost = True
			Call insPrintCollectionRep("COL005")
			
			'+ COL007: Reporte de control de cheques diferidos.
		Case "COL007"
			lblnPost = True
			Call insPrintCollectionRep("COL007")
			
			'+ COL009: Reporte de Anulación Automática.
		Case "COL009"
			If CStr(Session("BatchEnabled")) <> "1" Then
				lclsCollectionRep = New eCollection.CollectionRep
				With Request
					lblnPost = lclsCollectionRep.insPostCOL009(mobjValues.StringToType(.Form.Item("optProcess"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
				End With
				
				If lblnPost Then
					mstrKey = lclsCollectionRep.lstrKey
					Call insPrintCollectionRep("COL009")
				End If
			Else
				lclsBatch_param = New eSchedule.Batch_param
				With lclsBatch_param
					.nBatch = 104
					.nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong)
					'+Parametros de proceso			        
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("optProcess"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
					'+Parametros del resultado
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("optProcess"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
					.Save()
				End With
				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				lclsBatch_param = Nothing
				
				lblnPost = True
				
			End If
			
			'+ COL011: Reporte de Desglose de recibos cobrados.
		Case "COL011"
			lblnPost = True
			Call insPrintCollectionRep("COL011")
			
			'+ COL500: Generación automática de cobranzas.
            Case "COL500"
                
                If CStr(Session("BatchEnabled")) <> "1" Then
                    lblnPost = True
                    lclsCollectionRep = New eCollection.CollectionRep
                    With Request
                        lblnPost = lclsCollectionRep.insPostCOL500("COL500", mobjValues.StringToType(.Form.Item("tcdExpirDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optGenera"), .Form.Item("optCurrency"), .Form.Item("optProcess"), mobjValues.StringToType(.Form.Item("tcdIncrease"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkTakeOld"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), .Form.Item("valClient"), mobjValues.StringToType(.Form.Item("cbeTyp_CreCard"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True))
                        mstrProcess = lclsCollectionRep.sProcess
                        mstrNoProcess = lclsCollectionRep.sNoProcess
                        mstrKey = lclsCollectionRep.lstrKey
                        mstrFileName = lclsCollectionRep.sFileName
                        mstrAgreeApvSef = lclsCollectionRep.sAgreeApvSef
					
                        If mstrProcess = "1" Or mstrNoProcess = "1" Then
                            Call insPrintCollectionRep("COL500")
                        Else
                            lclsQuery = New eRemoteDB.Query
                            If lclsQuery.OpenQuery("Message", "sMessaged", "nErrornum = 20024") Then
                                Response.Write("<SCRIPT>alert('" & lclsQuery.FieldToClass("sMessaged") & "');</" & "Script>")
                            End If
                            lclsQuery = Nothing
                        End If
                    End With
                Else
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 101
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong)
                        '+Parametros de proceso			        
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdExpirDat"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optGenera"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optCurrency"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optProcess"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdIncrease"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUsercode)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("chkTakeOld"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("valClient"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeTyp_CreCard"), eFunctions.Values.eTypeData.etdLong,True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True))
					
                        '+Parametros del resultado
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("optGenera"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdExpirDat"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdIncrease"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("optProcess"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("optCurrency"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
				
                    lblnPost = True
                End If
			
                '+ COL502: Imputación de PAC/TRANSBANK.
		Case "COL502"
			lblnPost = True
			
			lclsCollectionRep = New eCollection.CollectionRep
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				With Request
					mstrString = "&nInsur_Area=" & .Form.Item("cbeInsur_area") & "&nWay_Pay=" & .Form.Item("cbeWay_pay") & "&dLimit_pay=" & .Form.Item("tcdLimit_pay") & "&dPayDate=" & .Form.Item("tcdPayDate")
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					With Request
						lblnPost = lclsCollectionRep.insPostTCOL502Upd("Actualizar", mobjValues.StringToType(.Form.Item("tcnId_Register"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommiss"), eFunctions.Values.eTypeData.etdDouble))
					End With
				Else
					If CStr(Session("BatchEnabled")) <> "1" Then
						lclsGeneralFunction = New eGeneral.GeneralFunction
						mstrKey = lclsGeneralFunction.getsKey(Session("nUsercode"))
						lclsGeneralFunction = Nothing
						With Request
							llngSize = Request.Form.Item("nCount")
							For lintCountCOL502 = 1 To CInt(.Form.Item("nCount"))
								If .Form.GetValues("sSelected").GetValue(lintCountCOL502 - 1) = "1" Then
									lblnPost = lclsCollectionRep.insPostCOL502(mstrKey, mobjValues.StringToType(.Form.Item("hddnInsur_Area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnWay_Pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hdddLimit_pay"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("hdddPayDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.GetValues("hddnBank").GetValue(lintCountCOL502 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("nAcc_Bankhdr").GetValue(lintCountCOL502 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hdddEffecdate").GetValue(lintCountCOL502 - 1), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.GetValues("hddnMovement").GetValue(lintCountCOL502 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnAmount").GetValue(lintCountCOL502 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnCommission").GetValue(lintCountCOL502 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
									If lblnPost Then
										mintCount = lintCountCOL502
									End If
								End If
							Next 
						End With
						If lblnPost Then
							mstrFileName = lclsCollectionRep.sFileName
							Call insPrintCollectionRep("COL502")
						Else
							Response.Write("<SCRIPT>alert('Error: No se Generaron Datos');</" & "Script>")
						End If
						' End If
					Else
						
						llngSize = Request.Form.Item("nCount")
						For lintCountCOL502 = 1 To llngSize
							If Request.Form.GetValues("sSelected").GetValue(lintCountCOL502 - 1) = "1" Then
								lclsBatch_param = New eSchedule.Batch_param
								With lclsBatch_param
									.nBatch = 102
									.nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong)
									'+Parametros de proceso			        
									.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
									.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("hddnInsur_Area"), eFunctions.Values.eTypeData.etdDouble, True))
									.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("hddnWay_Pay"), eFunctions.Values.eTypeData.etdDouble, True))
									.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("hdddLimit_pay"), eFunctions.Values.eTypeData.etdDate, True))
									.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("hdddPayDate"), eFunctions.Values.eTypeData.etdDate, True))
									.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.GetValues("hddnBank").GetValue(lintCountCOL502 - 1), eFunctions.Values.eTypeData.etdDouble, True))
									.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.GetValues("nAcc_Bankhdr").GetValue(lintCountCOL502 - 1), eFunctions.Values.eTypeData.etdDouble, True))
									.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.GetValues("hdddEffecdate").GetValue(lintCountCOL502 - 1), eFunctions.Values.eTypeData.etdDate, True))
									.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.GetValues("hddnMovement").GetValue(lintCountCOL502 - 1), eFunctions.Values.eTypeData.etdDouble, True))
									.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.GetValues("hddnAmount").GetValue(lintCountCOL502 - 1), eFunctions.Values.eTypeData.etdDouble, True))
									.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.GetValues("hddnCommission").GetValue(lintCountCOL502 - 1), eFunctions.Values.eTypeData.etdDouble, True))
									.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUserCode)
									'+Parametros del resultado
									.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                                        .Save()
                                        ' Si se tiene mas de un registro seleccionado en el grid, la ejecucin del FOR es tan rapida
                                        ' que no permite generar un KEY distinto para cada uno y da error de PK en Batch_job
                                        ' Esta es la razon de hacer el delay en la ejecucion.                                        ' 
                                        System.Threading.Thread.Sleep(1000)
								End With
								Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
								lclsBatch_param = Nothing
							End If
						Next 
						
						lblnPost = True
					End If
				End If
			End If
			
			'+ COL507: Imputación de pagos en ventanilla.
		Case "COL507"
			If CStr(Session("BatchEnabled")) <> "1" Then
				lblnPost = True
				With Request
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						Call insPrintCollectionRep("COL507")
					End If
				End With
			Else
				lclsBatch_param = New eSchedule.Batch_param
				With lclsBatch_param
					.nBatch = 124
					.nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong)
					'+Parametros de proceso			        
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjUploadRequest.Item("valBank").Item("Value"))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjUploadRequest.Item("valAcc_number").Item("Value"))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjUploadRequest.Item("tcdPayDate").Item("Value"))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjUploadRequest.Item("tcnAmountPay").Item("Value"))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUserCode)
					'+Parametros del resultado
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjUploadRequest.Item("tcdPayDate").Item("Value"))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
					.Save()
				End With
				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				lclsBatch_param = Nothing
				
				lblnPost = True
				
			End If
			
			'+ COL511: Generación automática de cobranzas.
		Case "COL511"
			lblnPost = True
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lclsCollectionRep = New eCollection.CollectionRep
					If Not lclsCollectionRep.insPostCOL511("COL511", mobjValues.StringToType(mobjUploadRequest.Item("valBank").Item("Value"), eFunctions.Values.eTypeData.etdDouble, True), mstrPath & mobjUploadRequest.Item("tctName").Item("FileName"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True)) Then
					End If
				End If
			End With
			
			'+ COL556: Conciliación automática de primas recaudadas.
		Case "COL556"
			If CStr(Session("BatchEnabled")) <> "1" Then
				lclsCollectionRep = New eCollection.CollectionRep
				lclsGeneralFunction = New eGeneral.GeneralFunction
				mstrKey = lclsGeneralFunction.getsKey(Session("nUsercode"))
				lclsGeneralFunction = Nothing
				
				lblnPost = lclsCollectionRep.insPostCOL556(mstrKey, mobjValues.StringToType(Request.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("optProcessTyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdOperdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				If lblnPost Then
					mstrFileName = lclsCollectionRep.sFileName
					mstrFileName1 = lclsCollectionRep.sFileName1
					Call insPrintCollectionRep("COL556")
				End If
			Else
				lclsBatch_param = New eSchedule.Batch_param
				With lclsBatch_param
					.nBatch = 116
					.nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong)
					'+Parametros de proceso			        
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble, True))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("optProcessTyp"), eFunctions.Values.eTypeData.etdDouble, True))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdOperdate"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUserCode)
					
					'+Parametros del resultado
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
					.Save()
				End With
				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				lclsBatch_param = Nothing
				
				lblnPost = True
				
			End If
			
			
			'+ COL585: Proceso de pareo de mandatos.
		Case "COL585"
			lblnPost = True
			With Request
				
				
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Call insPrintCollectionRep("COL585")
				End If
				
			End With
			
			'+ COL594: Re-impresión y anulación de boletines.
		Case "COL594"
			lblnPost = True
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					If CDbl(.Form.Item("optOper")) = 1 Then
						lclsGeneralFunction = New eGeneral.GeneralFunction
						mstrKey = lclsGeneralFunction.getsKey(Session("nUsercode"))
						lclsGeneralFunction = Nothing
						Call insPrintCollectionRep("COL594")
					Else
						lclsCollectionRep = New eCollection.CollectionRep
						If lclsCollectionRep.insPostCOL594("COL594", mobjValues.StringToType(.Form.Item("optOper"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdCollDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcnBullStart"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnBullEnd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCancel_Cod"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True)) Then
							mstrKey = lclsCollectionRep.lstrKey
						End If
					End If
				End If
			End With
			
			'+ COL626: Reporte de recibos para un convenio.
		Case "COL626"
			lblnPost = True
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Call insPrintCollectionRep("COL626")
				End If
			End With
			
			'+ COL628: Cierre de facturación. 
		Case "COL628"
			lblnPost = True
			With Request
				lclsCollectionRep = New eCollection.CollectionRep
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					If Not lclsCollectionRep.inspostCOL628("COL628", mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdLastClosed"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True)) Then
					End If
				End If
			End With
			
			'+ COL636: Pago de comisión de cobradores. 
		Case "COL636"
			If CStr(Session("BatchEnabled")) <> "1" Then
				lblnPost = True
				With Request
					lclsCollectionRep = New eCollection.Premium_mo
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						If lclsCollectionRep.inspostCOL636(mobjValues.StringToType(.Form.Item("cbeInsurArea"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCollectorType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True)) Then
							Call insPrintCollectionRep("COL636")
						End If
					End If
				End With
				
			Else
				lclsBatch_param = New eSchedule.Batch_param
				With lclsBatch_param
					.nBatch = 34
					.nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong)
					'+Parametros de proceso			        
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeInsurArea"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeCollectorType"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUserCode)
					'+Parametros del resultado
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdInitDate"))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
					.Save()
				End With
				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				lclsBatch_param = Nothing
				
				lblnPost = True
				
			End If
			
			'+ COL684: Traspaso de cartera de cobradores. 
		Case "COL684"
			lblnPost = True
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Call insPrintCollectionRep("COL684")
				End If
			End With
			
			'+ COL686: Preparación de ctas. ctes. para los cobradores.
		Case "COL686"
			If CStr(Session("BatchEnabled")) <> "1" Then
				lblnPost = True
				With Request
					lclsCollectionRep = New eCollection.Premium_mo
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						If lclsCollectionRep.inspostCOL686(mobjValues.StringToType(.Form.Item("cbeInsurArea"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdFinalDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCollectorType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("optExecute"), eFunctions.Values.eTypeData.etdDouble)) Then
							
							sKey_Col686 = lclsCollectionRep.sKey
							
							Call insPrintCollectionRep("COL686")
						End If
					End If
				End With
			Else
				lclsBatch_param = New eSchedule.Batch_param
				With lclsBatch_param
					.nBatch = 60
					.nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong)
					'+Parametros de proceso
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeInsurArea"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdFinalDate"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeCollectorType"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("optExecute"), eFunctions.Values.eTypeData.etdDouble))
					'+Parametros del resultado
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("optExecute"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("tcdFinalDate"), eFunctions.Values.eTypeData.etdDate))
					.Save()
				End With
				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				lclsBatch_param = Nothing
				lblnPost = True
			End If
			
			'+ COL704: Imputación Automática de rechazos. 
		Case "COL704"
			lblnPost = True
			With Request
				lclsCollectionRep = New eCollection.CollectionRep
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					
					lblnPost = lclsCollectionRep.insPostCOL704(mobjValues.StringToType(Request.Form.Item("cbeWayPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mstrKey, mobjValues.StringToType(Request.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdPayDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
			'+ COL723: Impresión de mandatos y sus polizas.
		Case "COL723"
			lblnPost = True
			
			If Request.Form.Item("chkReuse") <> "2" Then
				lintchkReuse = 1
			Else
				lintchkReuse = mobjValues.StringToType(Request.Form.Item("chkReuse"), eFunctions.Values.eTypeData.etdDouble, True)
			End If
			
			If Request.Form.Item("cbeBranch") = vbNullString Then
				lintcbeBranch = 0
			Else
				lintcbeBranch = Request.Form.Item("cbeBranch")
			End If
			
			lclsCollectionRep = New eCollection.CollectionRep
			With Request
				lblnPost = lclsCollectionRep.insPostCOL723("COL723", mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(lintcbeBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lintchkReuse), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeIntention"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
				
			End With
			If lblnPost Then
				With Request
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						Call insPrintCollectionRep("COL723")
					End If
				End With
			Else
				lclsQuery = New eRemoteDB.Query
				If lclsQuery.OpenQuery("Message", "sMessaged", "nErrornum = 20024") Then
					Response.Write("<SCRIPT>alert('" & lclsQuery.FieldToClass("sMessaged") & "');</" & "Script>")
				End If
				lclsQuery = Nothing
			End If
			
			'+ COL742: Imputación automática de descuento por planilla.
		Case "COL742"
			lblnPost = True
			With Request
				lclsCollectionRep = New eCollection.CollectionRep
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					If lclsCollectionRep.inspostCOL742(mobjValues.StringToType(.Form.Item("cbeInsurArea"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdCollectDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True)) Then
						Call insPrintCollectionRep("COL742")
					Else
						lblnPost = False
					End If
					
				End If
			End With
			
			'+ COL777: Imputación automática de descuento por planilla.
		Case "COL777"
			lblnPost = True
			With Request
				lclsCollectionRep = New eCollection.CollectionRep
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					If lclsCollectionRep.inspostCOL777(mobjValues.StringToType(.Form.Item("tcdCollectIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdCollectEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True)) Then
						lblnPost = True
					Else
						lblnPost = False
					End If
				End If
			End With
			
			'+ COL832: Inventario primeras primas.
		Case "COL832"
			lblnPost = True
			With Request
				lclsColRepCOL832 = New eCollection.CollectionRep
				If lclsColRepCOL832.insPostCOL832_Res(mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeIniMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIniYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePerMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPerYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True)) Then
					
					lblnPost = True
					Session("mstrKey") = lclsColRepCOL832.lstrKey
					Call insPrintCollectionRep("COL832")
				Else
					lblnPost = False
				End If
			End With
			
			
			'+ COL910: Deudores por Prima.
		Case "COL910"
			lblnPost = True
			With Request
				lclsCollectionRep = New eCollection.CollectionRep
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					If lclsCollectionRep.insPostCOL910(mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdCollectDateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True)) Then
						
						mstrKey = lclsCollectionRep.lstrKey
						lblnPost = True
						Call insPrintCollectionRep("COL910")
					Else
						lblnPost = False
					End If
				End If
			End With
			
			'+ COL911: Provisión de morosidad Prima Pendiente.
		Case "COL911"
			lblnPost = True
			With Request
				lclsCollectionRep = New eCollection.CollectionRep
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					If lclsCollectionRep.insPostCOL911(mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdCollectDateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True)) Then
						
						mstrKey = lclsCollectionRep.lstrKey
						lblnPost = True
						Call insPrintCollectionRep("COL911")
					Else
						lblnPost = False
					End If
				End If
			End With
			
			'+ Reporte de pólizas rechadas.		
		Case "COL836"
			lblnPost = True
			Call insPrintCollectionRep("COL836")
			
			'+ VT00067 HAD080 Reporte de Operaciones de Cobranza
		Case "COL01166"
			lblnPost = True
			Call insPrintCollectionRep("COL01166")
			
	End Select
	
	insPostCollectionRep = lblnPost
	
	lclsCollectionRep = Nothing
	
End Function

'% insPrintCollectionRep: Se encarga de generar el reporte correspondiente.  
'--------------------------------------------------------------------------------------------  
Private Sub insPrintCollectionRep(ByRef Codispl As String)
	Dim Intermediario As Integer
	Dim Ramo As Integer
	Dim via_con_est As String
	Dim Estado As Integer
	Dim Fecha_Limite As String
	Dim Sin_Cob_Generada As String
	Dim Agencia As Integer
	Dim int_sup_age As String
	Dim Convenio As Integer
	Dim Via_Pago As Integer
	Dim fecha_Cob_Generada As String
	Dim ram_suc_mon As String
	Dim Moneda As Integer
	Dim Supervisor As Integer
	Dim nRecOri As Integer
        Dim Sucursal As Integer

        Dim lsIncreaseDate As String
        Dim lsExpiratDate as string
	'--------------------------------------------------------------------------------------------  
	Dim lstrdtmProcDate As Object
	mobjDocuments = New eReports.Report
	
	Dim mobjBath As eBatch.ValBatch
	Select Case Codispl
		
		'+ COL001: Operaciones de cobranza. 
		Case "COL001"
			With mobjDocuments
				.sCodispl = "COL001"
				.ReportFilename = "COL001.rpt"
				
				If mobjValues.StringToType(Request.Form.Item("hddnRecOri"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
					nRecOri = 0
				Else
					If mobjValues.StringToType(Request.Form.Item("hddnRecOri"), eFunctions.Values.eTypeData.etdDouble) = 2 Then
						nRecOri = 1
					Else
						nRecOri = 2
					End If
				End If
				
				.setStorProcParam(1, .setDate(Request.Form.Item("tcdInitDate")))
				.setStorProcParam(2, .setDate(Request.Form.Item("tcdEndDate")))
				.setStorProcParam(3, mobjValues.StringToType(CStr(nRecOri), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("cbeMovType"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(6, mobjValues.StringToType(Request.Form.Item("cbeInfoOrder"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(7, vbNullString)
				
				Response.Write((.Command))
			End With
			
		Case "COL003"
			With mobjDocuments
				.sCodispl = "COL003"
				Select Case Request.Form.Item("optDetail")
					'+ Con desglose.
					Case CStr(1)
						.ReportFilename = "COL003WithDetail.rpt"
						'+ Sin desglose. 
					Case CStr(2)
						.ReportFilename = "COL003WithOutDetail.rpt"
				End Select
				
				Select Case Request.Form.Item("optReceiptType")
					'+ Cobro
					Case CStr(1)
						.setStorProcParam(1, 1)
						'+ Devolución
					Case CStr(2)
						.setStorProcParam(1, 2)
						'+ Ambos
					Case CStr(3)
						.setStorProcParam(1, 0)
						
				End Select
				'+ Ramo              
				If mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
					Ramo = 0
				Else
					Ramo = mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
				End If
				
				'+ Sucursal      
				If mobjValues.StringToType(Request.Form.Item("cbeZone"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
					Sucursal = 0
				Else
					Sucursal = mobjValues.StringToType(Request.Form.Item("cbeZone"), eFunctions.Values.eTypeData.etdDouble)
				End If
				
				'+ Moneda
				If mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
					Moneda = 0
				Else
					Moneda = mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble)
				End If
				ram_suc_mon = Ramo & "|" & Sucursal & "|" & Moneda
				
				.setStorProcParam(2, ram_suc_mon)
				'+ Intermediario		
				If mobjValues.StringToType(Request.Form.Item("valAgentCode"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
					Intermediario = 0
				Else
					Intermediario = mobjValues.StringToType(Request.Form.Item("valAgentCode"), eFunctions.Values.eTypeData.etdDouble)
				End If
				
				'+ Supervisor		
				If mobjValues.StringToType(Request.Form.Item("valSupCode"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
					Supervisor = 0
				Else
					Supervisor = mobjValues.StringToType(Request.Form.Item("valSupCode"), eFunctions.Values.eTypeData.etdDouble)
				End If
				
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("optDetail"), eFunctions.Values.eTypeData.etdDouble))
				
				'+ Agencia. Se repite la sucursal mientras no se defina si se usará la agencia.	      
				If mobjValues.StringToType(Request.Form.Item("cbeZone"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
					Agencia = 0
				Else
					Agencia = mobjValues.StringToType(Request.Form.Item("cbeZone"), eFunctions.Values.eTypeData.etdDouble)
				End If
				int_sup_age = Intermediario & "|" & Supervisor & "|" & Agencia
				.setStorProcParam(4, int_sup_age)
				
				'+ Vía de pago.
				If mobjValues.StringToType(Request.Form.Item("cbeWayPay"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
					Via_Pago = 0
				Else
					Via_Pago = mobjValues.StringToType(Request.Form.Item("cbeWayPay"), eFunctions.Values.eTypeData.etdDouble)
				End If
				
				'+ Convenio
				If mobjValues.StringToType(Request.Form.Item("tcnAgreement"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
					Convenio = 0
				Else
					Convenio = mobjValues.StringToType(Request.Form.Item("tcnAgreement"), eFunctions.Values.eTypeData.etdDouble)
				End If
				
				'+ Estado del recibo
				If mobjValues.StringToType(Request.Form.Item("cbeNStatus_Pre"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
					Estado = 0
				Else
					Estado = mobjValues.StringToType(Request.Form.Item("cbeNStatus_Pre"), eFunctions.Values.eTypeData.etdDouble)
				End If
				via_con_est = Via_Pago & "|" & Convenio & "|" & Estado
				.setStorProcParam(5, via_con_est)
				'+ Fecha límite de pago	
				Fecha_Limite = .setDate(mobjValues.StringToType(Request.Form.Item("tcdLimitDate"), eFunctions.Values.eTypeData.etdDate))
				'+ Sin cobranza generada
				Sin_Cob_Generada = "1"
				fecha_Cob_Generada = Fecha_Limite & "|" & Sin_Cob_Generada
				
				.setStorProcParam(6, fecha_Cob_Generada)
				Response.Write((.Command))
				
			End With
			
			'+ COL005: Reporte de Cuadre de cobranzas. 
		Case "COL005"
			With mobjDocuments
				.sCodispl = "COL005"
				.ReportFilename = "infRCollectMatch.rpt"
				.setStorProcParam(1, .setDate(mobjValues.StringToType(Request.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate)))
				Response.Write((.Command))
			End With
			
			'+ COL007: Reporte de control de cheques diferidos.
		Case "COL007"
			With mobjDocuments
				.sCodispl = "COL007"
				
				If mobjValues.StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate) <> eRemoteDB.Constants.dtmNull Then
					.ReportFilename = "COL007Effect.rpt"
					.Tittle = "REPORTE DE CHEQUES A FECHA Y TARJETAS DE CREDITO"
					
					.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("valOffice"), eFunctions.Values.eTypeData.etdDouble, True))
					
					If mobjValues.StringToType(Request.Form.Item("valAgentCode"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
						.setStorProcParam(2, Request.Form.Item("valAgentCode"))
					Else
						.setStorProcParam(2, 0)
					End If
					
					.setStorProcParam(3, Mid(Request.Form.Item("tcdEffecDate"), 7, 4) & Mid(Request.Form.Item("tcdEffecDate"), 4, 2) & Mid(Request.Form.Item("tcdEffecDate"), 1, 2)) 'Ojo
					.setStorProcParam(4, 1)
				End If
				
				If mobjValues.StringToType(Request.Form.Item("tcdPendDate"), eFunctions.Values.eTypeData.etdDate) <> eRemoteDB.Constants.dtmNull Then
					.ReportFilename = "COL007Pend.rpt"
					.Tittle = "REPORTE DE CHEQUES A FECHA Y TARJETAS DE CREDITO"
					
					.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("valOffice"), eFunctions.Values.eTypeData.etdDouble, True))
					
					If mobjValues.StringToType(Request.Form.Item("valAgentCode"), eFunctions.Values.eTypeData.etdDouble) <> 0 Then
						.setStorProcParam(2, Request.Form.Item("valAgentCode"))
					Else
						.setStorProcParam(2, 0)
					End If
					
					.setStorProcParam(3, Mid(Request.Form.Item("tcdPendDate"), 7, 4) & Mid(Request.Form.Item("tcdPendDate"), 4, 2) & Mid(Request.Form.Item("tcdPendDate"), 1, 2)) 'Ojo 
					.setStorProcParam(4, 2)
				End If
				
				Response.Write((.Command))
			End With
			
			'+ COL009: Reporte de Anulación Automática. 
		Case "COL009"
			With mobjDocuments
				.ReportFilename = "COL009.rpt"
				.sCodispl = "COL009"
				.setParamField(1, "nTypeProce", mobjValues.StringToType(Request.Form.Item("optProcess"), eFunctions.Values.eTypeData.etdInteger))
				.setParamField(2, "sDateProcec", .setDate(Request.Form.Item("tcdProcessDate")))
				.setStorProcParam(1, mstrKey)
			End With
			Response.Write((mobjDocuments.Command))
			
			'+ COL011: Reporte de Desglose de Recibos Cobrados. 
		Case "COL011"
			With mobjDocuments
				.ReportFilename = "infRReceiptDetail.rpt"
				.sCodispl = "COL011"
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(2, .setDate(Request.Form.Item("tcdinitDate")))
				.setStorProcParam(3, .setDate(Request.Form.Item("tcdendDate")))
			End With
			Response.Write((mobjDocuments.Command))
			
			'+ COL500: Generación automática de cobranzas.
            Case "COL500"
                
                With mobjDocuments
                    If mstrProcess = "1" Then
                        '+ Reporte de Información procesada 	    			
                        .sCodispl = "COL500"
                        .ReportFilename = "COL500E.rpt"
                        
                        lsIncreaseDate = .setdate(Request.Form.Item("tcdIncrease"))
                        lsExpiratDate = .setdate(Request.Form.Item("tcdExpirdat"))

                        .setStorProcParam(1, mstrKey)
                        .setStorProcParam(2, lsIncreaseDate)
                        .setStorProcParam(3, Request.Form.Item("optProcess"))
                        .setStorProcParam(4, Request.Form.Item("optCurrency"))
                        .setStorProcParam(5, lsExpiratDate)
                        .setStorProcParam(6, Request.Form.Item("optGenera"))
                        .setStorProcParam(7, mstrFileName)
                        
                        Response.Write((.Command))
                        .Reset()
                        mblnTimeOut = True
					
                        ''+ Reporte Resumen Procesos de Generación de Cobranza 				
                        .sCodispl = "COL500"
                        .ReportFilename = "COL500_RES.rpt"
                        .setStorProcParam(1, mstrKey)
                        .setStorProcParam(2, .setdate(Request.Form.Item("tcdExpirdat")))
                        Response.Write((.Command))
                        .Reset()
                        mblnTimeOut = True
                    End If
				
                    If mobjValues.StringToType(Request.Form.Item("optProcess"), eFunctions.Values.eTypeData.etdDouble, True) = 2 Then
                        Select Case mobjValues.StringToType(Request.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True)
                            Case 1, 2
                                '+ Reporte de Errores generados  
                                If mstrNoProcess = "1" Then
                                    .sCodispl = "COL500"
                                    .ReportFilename = "COL500A.rpt"
                                    .setStorProcParam(1, mstrKey)
                                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True))
                                    .bTimeOut = True
                                    .nTimeOut = 5000
                                    Response.Write((.Command))
                                    .Reset()
                                    mblnTimeOut = True
                                    .sCodispl = "COL500"
                                End If
                            Case 3
                                '+ Planilla: Reporte de cargos realizados  
                                If mstrProcess = "1" Then
                                    If Right(mstrAgreeApvSef, 1) = "1" Then
                                        .ReportFilename = "COL500D.rpt"
                                        .setStorProcParam(1, mstrKey)
                                        .setStorProcParam(2, Request.Form.Item("optCurrency"))
                                        .bTimeOut = True
                                        .nTimeOut = 5000
                                        Response.Write((.Command))
                                        .Reset()
                                    End If
                                    '+ Planilla: Reporte de Comprobante de pago APV  
                                    If Left(mstrAgreeApvSef, 1) = "1" Then
                                        .ReportFilename = "COL500F.rpt"
                                        .setStorProcParam(1, mstrKey)
                                        .setStorProcParam(2, .setdate(Request.Form.Item("tcdExpirdat")))
                                        .bTimeOut = True
                                        .nTimeOut = 5000
                                        Response.Write((.Command))
                                        .Reset()
                                    End If
                                    '+ Planilla: Reporte de Comprobante depago SEF  		         		
                                    If Mid(mstrAgreeApvSef, 2, 1) = "1" Then
                                        .ReportFilename = "COL500G.rpt"
                                        .setStorProcParam(1, mstrKey)
                                        .setStorProcParam(2, .setdate(Request.Form.Item("tcdExpirdat")))
                                        .bTimeOut = True
                                        .nTimeOut = 5000
                                        Response.Write((.Command))
                                    End If
                                End If
                            Case 4
                                '+ Boletin: Reporte de Avisos de Cobranza 
                                If mstrNoProcess = "1" Then
                                    .sCodispl = "COL500"
                                    If mobjValues.insGetSetting("Active", "No", "CustomBillingNotice").ToUpper = "YES" Then
                                        .ReportFilename = "COL500_CUPON.rpt"
                                    Else
                                        .ReportFilename = "COL701a.rpt"
                                    End If
                                    .setStorProcParam(1, Request.Form.Item("cbeWay_pay"))
                                    .setStorProcParam(2, Request.Form.Item("cbeInsur_area"))
                                    .setStorProcParam(3, Request.Form.Item("valAgreement"))
                                    .setStorProcParam(4, .setdate(Request.Form.Item("tcdExpirDat")))
                                    .setStorProcParam(5, vbNullString)
                                    .setStorProcParam(6, vbNullString)
                                    .setStorProcParam(7, "1")
                                    .setStorProcParam(8, "1")
                                    .setStorProcParam(9, mstrKey)
                                    .setStorProcParam(10, Session("nUsercode"))
                                    Response.Write(.Command)
                                    .Reset()
                                End If                                    
                        End Select
                    End If
                End With
			
                mobjDocuments = Nothing
                Server.ScriptTimeout = 90
			
                '+ COL502: Imputación PAC/TRANSBANK
		Case "COL502"
			mobjDocuments = Nothing
			mobjDocuments = New eReports.Report
			
			With mobjDocuments
				
				'+ Reporte Aceptados
				.ReportFilename = "COL502_EXE.rpt"
				.sCodispl = "COL502"
				.setStorProcParam(1, .setDate(mobjValues.StringToType(Request.Form.Item("hdddLimit_pay"), eFunctions.Values.eTypeData.etdDate, True)))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("hddnInsur_Area"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("hddnWay_Pay"), eFunctions.Values.eTypeData.etdDouble, True))
				
				If llngSize = 1 Then
					.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("hddnBank"), eFunctions.Values.eTypeData.etdDouble, True))
				Else
					.setStorProcParam(4, mobjValues.StringToType(Request.Form.GetValues("hddnBank").GetValue(mintCount - 1), eFunctions.Values.eTypeData.etdDouble))
				End If
				
				.setStorProcParam(5, 1)
				Response.Write((.Command))
				.Reset()
				mblnTimeOut = True
				mobjDocuments = Nothing
				
				'+ Reporte Rechazados
				
				If mstrFileName = vbNullString Then
					Response.Write("<SCRIPT>alert('No se Generó archivo de Rechazos');</" & "Script>")
				Else
					Response.Write("<SCRIPT>AbrirArchivo('" & mstrFileName & "');</" & "Script>")
				End If
				
				mobjDocuments = New eReports.Report
				.ReportFilename = "COL502_REJ.rpt"
				.sCodispl = "COL502"
				.setStorProcParam(1, .setDate(mobjValues.StringToType(Request.Form.Item("hdddLimit_pay"), eFunctions.Values.eTypeData.etdDate, True)))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("hddnInsur_Area"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("hddnWay_Pay"), eFunctions.Values.eTypeData.etdDouble, True))
				
				If llngSize = 1 Then
					.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("hddnBank"), eFunctions.Values.eTypeData.etdDouble, True))
				Else
					.setStorProcParam(4, mobjValues.StringToType(Request.Form.GetValues("hddnBank").GetValue(mintCount - 1), eFunctions.Values.eTypeData.etdDouble))
				End If
				
				.setStorProcParam(5, 2)
				.bTimeOut = True
				.nTimeOut = 5000
				Response.Write((.Command))
				mobjDocuments = Nothing
			End With
			
			'+ COL507: Imputación de pagos en ventanilla
		Case "COL507"
			
			With mobjDocuments
				.ReportFilename = "COL507.rpt"
				.sCodispl = "COL507"
				.setStorProcParam(1, mobjUploadRequest.Item("valBank").Item("Value"))
				.setStorProcParam(2, mobjUploadRequest.Item("valAcc_number").Item("Value"))
				.setStorProcParam(3, .setDate(mobjUploadRequest.Item("tcdPayDate").Item("Value")))
				.setStorProcParam(4, .setDate(mobjUploadRequest.Item("tcdLimit_pay").Item("Value")))
				.setStorProcParam(5, mobjUploadRequest.Item("tcnAmountPay").Item("Value"))
				.setStorProcParam(6, Session("nUsercode"))
				Response.Write((.Command))
			End With
			
			
			'+ COL556: Conciliación automática de primas recaudadas.        
		Case "COL556"
			With mobjDocuments
				'+ Reporte de procesados
				.ReportFilename = "COL556.rpt"
				.sCodispl = "COL556"
				.setStorProcParam(1, mstrKey)
				.setStorProcParam(2, 1)
				.setParamField(1, "sFileName", mstrFileName)
				Response.Write((.Command))
				.Reset()
				mblnTimeOut = True
				
				'+ Reporte de incidencias         
				.ReportFilename = "COL556.rpt"
				.sCodispl = "COL556"
				.setStorProcParam(1, mstrKey)
				.setStorProcParam(2, 0)
				.setParamField(1, "sFileName", mstrFileName1)
				.bTimeOut = True
				Response.Write((.Command))
			End With
			mobjDocuments = Nothing
			
			'+ COL585: Pareo de Mandatos 
		Case "COL585"
			With mobjDocuments
				.ReportFilename = "COL585.rpt"
				.sCodispl = "COL585"
				.setStorProcParam(1, mobjValues.StringToType(mstrvalBank, eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(2, .setDate(mobjValues.StringToType(mstrtProcDate, eFunctions.Values.eTypeData.etdDate)))
				Response.Write((.Command))
			End With
			
			'+ COL594: Re-impresión y anulación de boletines
		Case "COL594"
			With mobjDocuments
				.ReportFilename = "COL701A.rpt"
				.sCodispl = "COL701"
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(4, .setDate(Request.Form.Item("tcdCollDate")))
				.setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("tcnBullStart"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(6, mobjValues.StringToType(Request.Form.Item("tcnBullEnd"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(7, "1")
				.setStorProcParam(8, "3")
				.setStorProcParam(9, mstrKey)
				.setStorProcParam(10, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
				Response.Write((.Command))
				.Reset()
				mblnTimeOut = True
				
				'+ Se imprime el reporte COL701B
				.ReportFilename = "COL701B.rpt"
				.sCodispl = "COL701"
				.setStorProcParam(1, mstrKey)
				.bTimeOut = True
				.nTimeOut = 5000
				Response.Write((.Command))
				.Reset()
				
				'+ Se imprime el reporte COL701C
				.ReportFilename = "COL701C.rpt"
				.sCodispl = "COL701"
				.setStorProcParam(1, mstrKey)
				Response.Write((.Command))
			End With
			
			'+ COL626: Recibos asociados a un convenio de cobranza            
		Case "COL626"
			
			With mobjDocuments
				.sCodispl = "COL626"
				.ReportFilename = "COL626.rpt"
				'.SetStorProcParam 1, mobjValues.StringToType(Request.Form("cbeInsur_area"),eFunctions.Values.eTypeData.etdDouble)
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("optTyp_info"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, .setDate(mobjValues.StringToType(Request.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate)))
				Response.Write((.Command))
			End With
			
			'+ COL636: Pago de comisiones a cobradores
		Case "COL636"
			With mobjDocuments
				.ReportFilename = "COL636.rpt"
				.sCodispl = "COL636"
				Response.Write((.Command))
			End With
			
			'+ COL684: Traspaso de cartera de cobradores 
		Case "COL684"
			With mobjDocuments
				.ReportFilename = "COL684.rpt"
				.sCodispl = "COL684"
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeInsurArea"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valCollectorPre"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("valCollectorNew"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, .setDate(Request.Form.Item("tcdProcessDate")))
				.setStorProcParam(5, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
				Response.Write((.Command))
			End With
			
			'+ COL686: Preparación de cuenta corriente de cobradores
		Case "COL686"
			With mobjDocuments
				.ReportFilename = "COL686.rpt"
				.sCodispl = "COL686"
				If Request.Form.Item("optExecute") = "2" Then
					.setParamField(1, "nTypeprocess", "DEFINITIVO")
				ElseIf Request.Form.Item("optExecute") = "1" Then 
					.setParamField(1, "nTypeprocess", "PRELIMINAR")
				End If
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeInsurarea"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(2, .setDate(Request.Form.Item("tcdInitDate")))
				.setStorProcParam(3, .setDate(Request.Form.Item("tcdFinalDate")))
				.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("cbeCollectorType"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, sKey_Col686)
				Response.Write((.Command))
			End With
			
			
			'+ COL723: Impresión de mandatos y sus polizas            
		Case "COL723"
			
			If Request.Form.Item("chkReuse") <> "2" Then
				lintchkReuse = 1
			Else
				lintchkReuse = mobjValues.StringToType(Request.Form.Item("chkReuse"), eFunctions.Values.eTypeData.etdDouble, True)
			End If
			
			If Request.Form.Item("cbeBranch") = vbNullString Then
				lintcbeBranch = 0
			Else
				lintcbeBranch = Request.Form.Item("cbeBranch")
			End If
			
			With mobjDocuments
				.ReportFilename = "COL723.rpt"
				.sCodispl = "COL723"
				.setStorProcParam(1, .setDate(Request.Form.Item("tcdInitDate")))
				.setStorProcParam(2, .setDate(Request.Form.Item("tcdEndDate")))
				.setStorProcParam(3, mobjValues.StringToType(lintcbeBranch, eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, mobjValues.StringToType(CStr(lintchkReuse), eFunctions.Values.eTypeData.etdDouble))
				Response.Write((.Command))
			End With
			
			'+ COL742: Imputación automática de descuento por planilla            
		Case "COL742"
			mobjDocuments = Nothing
			mobjDocuments = New eReports.Report
			
			With mobjDocuments
				.ReportFilename = "COL742_EXE.rpt"
				.sCodispl = "COL742"
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeInsurArea"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(2, .setDate(Request.Form.Item("tcdCollectDate")))
				.setStorProcParam(3, "2")
				Response.Write((.Command))
				.Reset()
				mblnTimeOut = True
				
				.ReportFilename = "COL742_REJ.rpt"
				.sCodispl = "COL742"
				.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("cbeInsurArea"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, .setDate(Request.Form.Item("tcdCollectDate")))
				.setStorProcParam(6, "1")
				.bTimeOut = True
				.nTimeOut = 5000
				Response.Write((.Command))
			End With
			
			'+ en caso de exitir data despliega el reporte de exdente tributario
			mobjBath = New eBatch.ValBatch
			If mobjBath.valRepCol742 Then
				With mobjDocuments
					.ReportFilename = "col742_exedente.rpt"
					.sCodispl = "COL742"
					.bTimeOut = True
					.nTimeOut = 5000
					Response.Write((.Command))
				End With
			End If
			mobjBath = Nothing
			
			'+ COL832: Inventario primeras primas.
		Case "COL832"
			With mobjDocuments
				.ReportFilename = "col832_1.rpt"
				.sCodispl = "COL832"
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbePerMonth"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnPerYear"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, Session("mstrKey"))
				Response.Write((mobjDocuments.Command))
				.Reset()
				mblnTimeOut = True
				.ReportFilename = "col832_2.rpt"
				.sCodispl = "COL832"
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbePerMonth"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnPerYear"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, Session("mstrKey"))
				.bTimeOut = True
				.nTimeOut = 5000
				Response.Write((mobjDocuments.Command))
				.Reset()
				mblnTimeOut = True
				.ReportFilename = "col832_3.rpt"
				.sCodispl = "COL832"
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbePerMonth"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnPerYear"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, Session("mstrKey"))
				.bTimeOut = True
				.nTimeOut = 10000
				Response.Write((mobjDocuments.Command))
				.Reset()
				mblnTimeOut = True
				.ReportFilename = "col832_4.rpt"
				.sCodispl = "COL832"
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbePerMonth"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnPerYear"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, Session("mstrKey"))
				.bTimeOut = True
				.nTimeOut = 15000
				Response.Write((mobjDocuments.Command))
				.Reset()
				mblnTimeOut = True
				.ReportFilename = "col832_5.rpt"
				.sCodispl = "COL832"
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbePerMonth"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnPerYear"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, Session("mstrKey"))
				.bTimeOut = True
				.nTimeOut = 20000
				Response.Write((mobjDocuments.Command))
				.Reset()
				mblnTimeOut = True
				.ReportFilename = "col832_6.rpt"
				.sCodispl = "COL832"
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbeIniMonth"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("tcnIniYear"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(6, mobjValues.StringToType(Request.Form.Item("cbePerMonth"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(7, mobjValues.StringToType(Request.Form.Item("tcnPerYear"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(8, Session("mstrKey"))
				.bTimeOut = True
				.nTimeOut = 20000
				Response.Write((mobjDocuments.Command))
				.Reset()
			End With
			
			'+ COL910: Deudores por Prima            
		Case "COL910"
			mobjDocuments = Nothing
			mobjDocuments = New eReports.Report
			
			With mobjDocuments
				.ReportFilename = "COL910.rpt"
				.sCodispl = "COL910"
				.setStorProcParam(1, mstrKey)
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcdCollectDateEnd"), eFunctions.Values.eTypeData.etdDate))
				Response.Write((.Command))
				mblnTimeOut = True
			End With
			
			'+ COL911: Provisión de morosidad Prima Pendiente
		Case "COL911"
			mobjDocuments = Nothing
			mobjDocuments = New eReports.Report
			
			With mobjDocuments
				.ReportFilename = "COL911.rpt"
				.sCodispl = "COL911"
				.setStorProcParam(1, mstrKey)
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcdCollectDateEnd"), eFunctions.Values.eTypeData.etdDate))
				Response.Write((.Command))
				mblnTimeOut = True
			End With
			
			
			mblnTimeOut = True
			
			'+ COL911: Provisión de morosidad Prima Pendiente
		Case "COL836"
			mobjDocuments = Nothing
			mobjDocuments = New eReports.Report
			
			With mobjDocuments
				.ReportFilename = "COL836.rpt"
				.sCodispl = "COL836"
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(6, mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble))
				Response.Write((.Command))
				mblnTimeOut = True
			End With
			
			mblnTimeOut = True
			
			'+ VT00067 HAD080 Reporte de Operaciones de Cobranza
		Case "COL01166"
			With mobjDocuments
				.sCodispl = sCodispl
				.ReportFilename = sCodispl & ".rpt"
				.setStorProcParam(1, nBranch)
				.setStorProcParam(2, nMovType)
				.setStorProcParam(3, nUserCode)
				.setStorProcParam(4, Request.Form.Item("tcdIniDate"))
				.setStorProcParam(5, Request.Form.Item("tcdEndDate"))
				'Response.Write(.Command)
				
			End With
			Response.Write((mobjDocuments.Command))
	End Select
	mobjDocuments = Nothing
End Sub

'% insUpLoadFile: Se encarga de subir el archivo seleccionado al servidor según ruta pasada como parámetro.
'% FilePath: Ruta física donde se va almacenar el archivo en el servidor. Eje. "c:\InetPub\UpLoad\"
'--------------------------------------------------------------------------------------------
Function insUpLoadFile(ByRef FilePath As String) As Boolean
	'--------------------------------------------------------------------------------------------
	Dim llngForWriting As Integer
	Dim llngLenBinary As Integer
	Dim lstrBoundry As String
	Dim llngBoundryPos As Integer
	'Dim lstrFileName As String
	Dim lbytByteCount As Integer
	Dim lbytRequestBin() As Byte
	Dim lbytboundary As Object
	Dim llngPosFile As Object
	Dim mobjFormFile As eCollection.FormFile
	Dim llngBoundryPosaux As Integer
	Dim lstrcbeInsur_Area As String
	Dim lstrtcdPayDate As String
	Dim lstrcbeWayPay As Object
	Dim lclsCollectionRep As eCollection.CollectionRep
	Dim lstrvalBank As Object
	Dim oFile as System.IO.File
    Dim oWrite as System.IO.StreamWriter
    Dim lstrvalAgreement as String
	
	llngForWriting = 2
	llngBoundryPos = 0
	llngBoundryPosaux = 0
	lbytByteCount = Request.TotalBytes
	lbytRequestBin = Request.BinaryRead(lbytByteCount)
	lstrBoundry = Request.ServerVariables.Item("HTTP_CONTENT_TYPE")
	llngBoundryPos = InStr(1, lstrBoundry, "boundary=") + 8
	
	If llngBoundryPos <> 8 Then
		llngBoundryPosaux = InStr(llngBoundryPos, lstrBoundry, "boundary=") + 8
	End If
	
	If llngBoundryPosaux <> 8 Then
		lstrBoundry = "--" & Right(lstrBoundry, Len(lstrBoundry) - llngBoundryPosaux)
	Else
		lstrBoundry = "--" & Right(lstrBoundry, Len(lstrBoundry) - llngBoundryPos)
	End If
	
    If True Then
		mobjFormFile = New eCollection.FormFile
		mobjFormFile.iBoundary = lstrBoundry
		mobjFormFile.iStreamBuffer = lbytRequestBin.Clone()

		If mobjFormFile.Request("tctFile") = vbCrLf Or mobjFormFile.Request("tctFile") = VbNullString Then
			lstrFileName = vbNullString
		Else
            If Not String.IsNullOrEmpty(Request.Form("hdsFileName")) Then
                lstrFileName = Request.Form("hdsFileName")
            Else
                lstrFileName = mobjFormFile.getRandomFilename(Session("NUSERCODE"), CStr(False))  + ".txt"              
            End If
			
			oWrite = oFile.CreateText(mstrPath & lstrfilename)
			oWrite.Write(mobjFormFile.Request("tctFile"))
            
		    If Request.QueryString.Item("sCodispl") = "COL704" Then
			    lstrcbeInsur_Area = mobjFormFile.Request("cbeInsur_Area")
			    lstrtcdPayDate = mobjFormFile.Request("tcdPayDate")
			    mstrcbeWayPay = mobjFormFile.Request("cbeWayPay")
			    mstrvalBank = mobjFormFile.Request("valBank")
                lstrvalAgreement = mobjFormFile.Request("valAgreement")
		    Else
			    mstrvalBank = mobjFormFile.Request("valBank")
			    mstrtProcDate = mobjFormFile.Request("tcdProcDate")                
			    lstrfilename = mstrPath & lstrfilename
		    End If

            oWrite.Close() 
		End If

		mstrFileName = lstrFileName
		mobjFormFile = Nothing
	End If
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ COL704: Imputación automática de rechazos.        
		Case "COL704"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				lclsCollectionRep = New eCollection.CollectionRep
				mstrErrorsUpload = lclsCollectionRep.insValCOL704("COL704", mobjValues.StringToType(lstrcbeInsur_Area, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrtcdPayDate, eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(mstrcbeWayPay, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mstrvalBank, eFunctions.Values.eTypeData.etdDouble), lstrFileName, mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrvalAgreement, eFunctions.Values.eTypeData.etdInteger))
                 
                If mstrErrorsUpload = VbNullString Then
                    insUpLoadFile = True
                Else
                    insUpLoadFile = False
                End If
				
			End If
			
			'+ Pareo de mandatos
		Case "COL585"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				lclsCollectionRep = New eCollection.CollectionRep
				mstrErrorsUpload = lclsCollectionRep.insValCOL585("COL585", mobjValues.StringToType(mstrtProcDate, eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(mstrvalBank, eFunctions.Values.eTypeData.etdDouble), mstrFileName, mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) 

                If mstrErrorsUpload = VbNullString Then
                    insUpLoadFile = True
                Else
                    insUpLoadFile = False
                End If
			End If
		Case Else
			
			'insUpLoadFile = "insUpLoadFile: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
            insUpLoadFile = True
			
	End Select
	
	'+ Si no hubo error se asigna el sKey.
    If insUpLoadFile = True And Request.QueryString.Item("sCodispl") <> "COL507" Then
		mstrKey = lclsCollectionRep.lstrKey
	End If
	
	lclsCollectionRep = Nothing
	'	End If
	
End Function

'% getString: Conversión de los datos de Byte a String
'--------------------------------------------------------------------------------------------
Function getString(ByRef sStringBin As String) As String
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Integer
	getString = vbNullString
	
'UPGRADE_ISSUE: LenB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
	For lintCount = 1 To Len(sStringBin)
'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
'UPGRADE_ISSUE: AscB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
		getString = getString & CStr(Asc(Mid(sStringBin, lintCount, 1)))
	Next 
	
End Function

</script>
<%Response.Expires = -1
Server.ScriptTimeOut = 1800
mobjValues = New eFunctions.Values
sCodispl = UCase(Request.QueryString.Item("sCodispl"))

'+ Se inicializa retardo del refresco de la pagina 
mblnTimeOut = False

nBranch = mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong)
nProduct = mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong)
nPolicy = mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdLong, True)
nClaim = mobjValues.StringToType(Request.Form.Item("tcnClaim"), eFunctions.Values.eTypeData.etdLong, True)
nCertif = mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdLong, True)
sClient = Request.Form.Item("dtcClient")
nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong)
nMovType = mobjValues.StringToType(Request.Form.Item("cbeMovType"), eFunctions.Values.eTypeData.etdLong, True)
dIniDate = mobjValues.StringToType(Request.Form.Item("tcdIniDate"), eFunctions.Values.eTypeData.etdDate)
dEndDate = mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate)

If nBranch <= 0 Then nBranch = 0
If nProduct <= 0 Then nProduct = 0
If nPolicy <= 0 Then nPolicy = 0
If nClaim <= 0 Then nClaim = 0
If nCertif <= 0 Then nCertif = 0
If nMovType <= 0 Then nMovType = 0

%> 
<HTML>  
<HEAD>  
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">  
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>  
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>

  



  	  

   
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 44 $|$$Date: 28/09/04 16:43 $|$$Author: Jfrugero $"
</SCRIPT>
<SCRIPT>
function openWindowChild(URL,left,width,height) 
{
	child = window.open();
	child.location.href = URL;
}
function AbrirArchivo(sfilename_aux)
{
	openWindowChild(sfilename_aux,'0','800','600');
}
//-->	
</SCRIPT>
<SCRIPT>
//% CancelErrors: Regresa a la Página Anterior
//------------------------------------------------------------------------------
function CancelErrors()
//------------------------------------------------------------------------------
{
    self.history.go(-1)
}

//% NewLocation: Establece la Localizacion de la Pagina que se este trabajando.
//------------------------------------------------------------------------------
function NewLocation(Source,Codisp)
//------------------------------------------------------------------------------
{
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
//------------------------------------------------------------------------------
function Validate()
//------------------------------------------------------------------------------
{
 alert("Debe existir al menos un Criterio de selección o campo lleno");
}


</SCRIPT>

</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If

'mstrPath = "D:\\AppServ\\InetPub\\UpLoad\\"
mstrPath = "C:\\VisualtimeNet\\Temp\\TFiles\\"
mstrCommand = "&sModule=Collection&sProject=CollectionRep&sCodisplReload=" & Request.QueryString.Item("sCodispl")

'+ Si no se han validado los campos de la página
If Request.QueryString.Item("sCodispl") <> "COL511" And Request.QueryString.Item("sCodispl") <> "COL507" And Request.QueryString.Item("sCodispl") <> "COL585" And Request.QueryString.Item("sCodispl") <> "COL704" Then
	
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insValCollectionRep
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
		mstrErrors = vbNullString
	End If
Else
	mstrErrors = insValCollectionRep
	Session("sErrorTable") = mstrErrors
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CollectionErrors"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostCollectionRep() Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.QueryString.Item("sCodispl") <> "COL511" And Request.QueryString.Item("sCodispl") <> "COL507" And Request.QueryString.Item("sCodispl") <> "COL585" And Request.QueryString.Item("sCodispl") <> "COL704" Then
					If Request.QueryString.Item("sCodispl") = "COL832" Then
						If mblnTimeOut Then
							Response.Write(("<SCRIPT>setTimeout('insReloadTop(true, false);',30000);</SCRIPT>"))
						Else
							Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
						End If
					Else
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							If mblnTimeOut Then
								Response.Write(("<SCRIPT>setTimeout('insReloadTop(true, false);',10000);</SCRIPT>"))
							Else
								Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
							End If
						Else
							Response.Write("<SCRIPT>opener.top.document.location.reload();window.close();</SCRIPT>")
						End If
					End If
				Else
					If mblnTimeOut Then
						Response.Write(("<SCRIPT>setTimeout('insReloadTop(true, false);',10000);</SCRIPT>"))
					Else
						Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
					End If
				End If
			Else
				If Request.QueryString.Item("sCodispl") = "COL502" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;window.close();</SCRIPT>")
					End If
				Else
					If Request.QueryString.Item("sCodispl") <> "COL511" And Request.QueryString.Item("sCodispl") <> "COL507" And Request.QueryString.Item("sCodispl") <> "COL585" And Request.QueryString.Item("sCodispl") <> "COL704" Then
						If Request.QueryString.Item("sCodispl") = "COL832" Then
							If mblnTimeOut Then
								Response.Write(("<SCRIPT>setTimeout('insReloadTop(true, false);',30000);</SCRIPT>"))
							Else
								Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
							End If
						Else
							If Request.Form.Item("sCodisplReload") = vbNullString Then
								If mblnTimeOut Then
									Response.Write(("<SCRIPT>setTimeout('insReloadTop(true, false);',10000);</SCRIPT>"))
								Else
									Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
								End If
							Else
								Response.Write("<SCRIPT>opener.top.document.location.reload();</SCRIPT>")
							End If
						End If
					Else
						If mblnTimeOut Then
							Response.Write(("<SCRIPT>setTimeout('insReloadTop(true, false);',10000);</SCRIPT>"))
						Else
							Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
						End If
					End If
				End If
			End If
		Else
			Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=302" & mstrString & "'</SCRIPT>")
		End If
	Else
		' vcortes 18.08.04
		Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>") 'vcortes 18.08.04
	End If
End If

mobjValues = Nothing
mobjUploadRequest = Nothing
%>
</BODY>
</HTML>




