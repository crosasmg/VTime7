<script language="VB" runat="Server">
'-Se definen las constantes globales para el manejo del tipo de relación de cobro
Const cstrCollect As String = "1" 'Cobro

Const cstrReturn As String = "2" 'Devolución

Const cstrConcil As String = "3" 'Conciliación


'-Se definen las constantes globales para el manejo de las acciones de la secuencia.
Const cstrAdd As String = "1" 'Agregar

Const cstrQuery As String = "2" 'Consultar

Const cstrUpdate As String = "3" 'Actualizar o recuperar

Const cstrCut As String = "4" 'Eliminar

'-Esta constante se usa en la secuencia de cobranza ya que se agrego una nueva 
'-acción para poder modificar una relación
Const cstrModify As String = "5" 'Modificar


'-Se definen las constantes globales para el manejo del tipo de intermediarios
Const clngProducer As Short = 1 ' Productor

Const clngOrganizer As Short = 10 ' Organizador

Const clngAgentReceptacle As Short = 20 ' Gestor de cobro

Const clngAgent As Short = 4 ' Agente


'-Se definen las constantes globales para la númeración de recibos y pólizas
Const cstrSysNumeGeneral As String = "1" 'General

Const cstrSysNumeBranch As String = "2" 'Ramo

Const cstrSysNumeBranchProduct As String = "3" 'Ramo/Producto


Dim lobjValues As eFunctions.Values


Function ShowTotals() As Double
	Dim lobjColFormref As eCollection.ColformRef
	lobjColFormref = New eCollection.ColformRef
	
	With lobjColFormref
		.nBordereaux = Session("nBordereaux")
		.sStatus = Session("sStatus")
		.dCollect = Session("dCollectDate")
		.dValueDate = Session("dValueDate")
		.nAction = Session("CO001_nAction")
		.sRelOrigi = Session("sRelOrigi")
		.calTotals()
		
		ShowTotals = System.Math.Round(.nTotalAmount + .nDifference - .nPaidAmount, 6)
		
		Response.Write("<SCRIPT>")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotCobDev','" & lobjValues.TypeToString(System.Math.Round(.nTotalAmount, 0), eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotIn','" & lobjValues.TypeToString(System.Math.Round(.nPaidAmount, 0), eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotSaldo','" & lobjValues.TypeToString(System.Math.Round(ShowTotals, 0), eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
		Response.Write("</" & "Script>")
		
	End With
	'UPGRADE_NOTE: Object lobjColFormref may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lobjColFormref = Nothing
End Function


Function ShowTotalsBulletins() As Double
	Dim lobjT_bulletins_det As eCollection.T_bulletins_det
        lobjT_bulletins_det = New eCollection.T_bulletins_det
        
        Dim mobjValues As eFunctions.Values = New eFunctions.Values
	
	With lobjT_bulletins_det
		.nBulletins = Session("nBulletins")
		.dCollectDate = Session("dCollectDate")
		.calTotalsBulletins()
		Response.Write("<SCRIPT>")
		ShowTotalsBulletins = .nTotalGeneral
		Response.Write("top.fraHeader.UpdateDiv('lblTotSaldo','" & mobjValues.TypeToString(ShowTotalsBulletins, eFunctions.Values.eTypeData.etdDouble, True, 6) & "');")
		Response.Write("</" & "Script>")
	End With
	
	'UPGRADE_NOTE: Object lobjT_bulletins_det may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lobjT_bulletins_det = Nothing
End Function

</script>
<%lobjValues = New eFunctions.Values
%>




