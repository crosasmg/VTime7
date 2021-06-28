<%@ Page Language="VB" explicit="true"  Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

    '**-Objetive: Object for the handling of LOG
    '-Objetivo: Objeto para el manejo de LOG
    Dim mobjNetFrameWork As eNetFrameWork.Layout

    '**-Objetive: The Object to handling the load values general functions is defined
    '-Objetivo: Objeto para el manejo de las funciones generales de carga de valores        
    Dim mobjValues As eFunctions.Values

    '**-Objetive: Definition of the object to handle the grid and its properties
    '-Objetivo: Se define la variable para el manejo del Grid de la ventana
    Dim mobjGridItin As eFunctions.Grid
    Dim mobjGridMerch As eFunctions.Grid

    '**-Objetive: The object to handling the page zones is defined
    '-Objeto: para el manejo de las zonas de la página
    Dim mobjMenues As eFunctions.Menues


    '**%Objetive: Defines the columns of the grid 
    '%Objetivo: Define las columnas del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader_Itin()
        '--------------------------------------------------------------------------------------------
        mobjGridItin = New eFunctions.Grid
	
        With mobjGridItin
            .sSessionID = Session.SessionID
            .sCodisplPage = Request.QueryString.Item("sCodispl")
            .sArrayName = "marrArrayItin"
        End With
	
        '**+ The columns of the grid are defined
        '+ Se definen las columnas del grid  
	
        With mobjGridItin.Columns
            .AddTextColumn(9484, "Etapa", "tcnStage", 3, CStr(0), True, "Código de la etapa que cubre el transporte", , , , True, 1)
            .AddTextColumn(9485, "Origen", "tctOrigen", 20, vbNullString, True, "Ciudad origen para la ruta cubierta", , , , , 2)
            .AddDateColumn(9486, "Fecha de salida", "tcdOrigindat", , True, "Fecha de salida para la etapa", , , , , 3)
            .AddTextColumn(9487, "Destino", "tctDestiny", 20, vbNullString, True, "Ciudad destino para la ruta cubierta", , , , , 4)
            .AddDateColumn(9488, "Fecha de llegada", "tcdDestindat", , True, "Fecha de llegada para la etapa", , , , , 5)
            If CStr(Session("sPolitype")) <> "1" Then
                .AddPossiblesColumn(9489, "Tipo de ruta", "valRoute", "tabTypRoute", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "insParamValues(this);", , , "Tipo de ruta asegurada", , 6)
                .AddPossiblesColumn(9490, "Tipo de transporte", "valTransport", "tabtypTransport", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "", True, , "Tipo de transporte de la ruta asegurada", , 7)
            Else
                .AddPossiblesColumn(9489, "Tipo de ruta", "valRoute", "Table8003", eFunctions.Values.eValuesType.clngWindowType, , False, , , , "insParamValuesInd(this);", , , "Tipo de ruta asegurada", , 6)
                .AddPossiblesColumn(9490, "Tipo de transporte", "valTransport", "Table6031", eFunctions.Values.eValuesType.clngWindowType, , False, , , , "", True, , "Tipo de transporte de la ruta asegurada", , 7)
            End If
            .AddTextColumn(9491, "Matrícula", "tctName", 20, "", True, "Matrícula o nombre del medio de transporte", , , , Request.QueryString.Item("nTyproute") = "1", 8)
            If Request.QueryString.Item("Type") = "PopUp" Then
                .AddAnimatedColumn(9492, vbNullString, "btnName", "/VTimeNet/images/FindPolicyOff.png", "Consulta de la matrícula o medio de transporte", , "ShowName_Licen()", Request.QueryString.Item("nTyproute") <> "1" And Request.QueryString.Item("nTyproute") <> "4")
            End If
            .AddNumericColumn(9493, "Valor", "tcnCapital", 18, CStr(0), True, "Valor de la mercancía que se transporta", True, 6, , , , True, 9)
            .AddHiddenColumn("tcnFrandedi", CStr(0))
            .AddTextColumn(9485, "Nota de Pedido", "tctPurchase_Order", 20, vbNullString, True, "Nota de Pedido", , , , , 2)
            .AddTextColumn(9485, "Número de Aplicación", "tctApplicationNumber", 20, vbNullString, True, "Número de Aplicación", , , , , 2)
         
           
        End With
	
        '**+ The general properties of the grid are defined
        '+ Se definen las propiedades generales del grid
        With mobjGridItin
            If CStr(Session("sPolitype")) <> "1" Then
                With .Columns("valRoute").Parameters
                    .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
			
                With .Columns("valTransport").Parameters
                    .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nTypRoute", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            End If
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = Request.QueryString.Item("sCodispl")
            .Codisp = Request.QueryString.Item("sCodispl")
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = .ActionQuery
            .Height = 410
            .Width = 350
            .Top = 55
            .Left = 170
            .bCheckVisible = False
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("tcnStage").EditRecord = True
            .sDelRecordParam = "nStage='+ marrArrayItin[lintIndex].tcnStage + '" & "&nInd=1" & "&nCurrency= ' + self.document.forms[0].cbeCurrency.value + '"
            .sEditRecordParam = "nInd=1" & "&nCurrency= ' + self.document.forms[0].cbeCurrency.value + '"
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
		
        End With
    End Sub

    '**%Objetive: Defines the columns of the grid 
    '%Objetivo: Define las columnas del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader_Merch()
        '--------------------------------------------------------------------------------------------
        Dim lintQuery As Byte
        mobjGridMerch = New eFunctions.Grid
	
        lintQuery = 0
        If mobjValues.ActionQuery Then
            lintQuery = 1
        End If
	
        With mobjGridMerch
            .sSessionID = Session.SessionID
            .sCodisplPage = Request.QueryString.Item("sCodispl")
        End With
	
        '**+ The columns of the grid are defined
        '+ Se definen las columnas del grid  
        'clngComboType
        With mobjGridMerch.Columns
            If CStr(Session("sPolitype")) <> "1" Then
                .AddPossiblesColumn(9495, "Mercancía", "valClass", "tabTran_Class", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "insParameter(this)", True, , "Clase de la mercancía", , 1)
                .AddPossiblesColumn(9496, "Embalaje", "valPacking", "tabTran_pack", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "insChargevalues()", True, , "Tipo de embalaje", , 2)
            Else
                .AddPossiblesColumn(9495, "Mercancía", "valClass", "tabTran_Class", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "insParameterInd(this)", False, , "Clase de la mercancía", , 1)
                .AddPossiblesColumn(9496, "Embalaje", "valPacking", "Table237", eFunctions.Values.eValuesType.clngWindowType, , False, , , , , False, , "Tipo de embalaje", , 2)
            End If
            .AddNumericColumn(9497, "Cantidad", "tcnQuantran", 5, , , "Número de elementos que se transportan", , , , , "insCalValue();", False, 3)
            .AddPossiblesColumn(9498, "Unidad", "cbeUnit", "Table6013", eFunctions.Values.eValuesType.clngComboType, , , , , , "", False, , "Unidad de capacidad de los elementos", , 4)
            .AddNumericColumn(9499, "Costo por unidad", "tcnCostUnit", 18, , , "Unidad o capacidad de peso de los elementos", True, 6, , , "insCalValue();", False, 5)
            .AddNumericColumn(9493, "Valor", "tcnCapital", 18, CStr(0), True, "Valor de la mercancía que se transporta", True, 6, , , , True, 6)
            .AddNumericColumn(9500, "Tasa", "tcnMerchRate", 9, CStr(0), True, "Tasa aplicada al valor de la mercancía", , 6, , , , False, 7)
            '.AddNumericColumn(9494, "Deducible", "tcnFrandedi", 18, CStr(0), True, "Importe de deducible de la mercancía que se transporta", , 6, , , , , 8)
            .AddHiddenColumn("tcnFrandedi", CStr(0))
            .AddHiddenColumn("tcnMerchandise", Request.QueryString.Item("nStageDet"))
            If Request.QueryString.Item("Type") = "PopUp" Then
                Call .AddButtonColumn(9501, "Notas", "SCA2-T", 0, True, False, , , , , "btnNotenum")
            Else
                Call .AddButtonColumn(9501, "Notas", "SCA2-T", 0, True, True, , , , , "btnNotenum")
            End If
            '**+ Images
            '+ Imágenes
            .AddHiddenColumn("tcnImagenum", CStr(0))
            .AddAnimatedColumn(9502, "Imágenes", "btnImages", "/VTimeNet/images/GenQue21.gif", "Imágenes", , "insShowImages(1, 0, 0, 0," & lintQuery & ");", , 8)
            .AddHiddenColumn("tctSel", CStr(0))
            .AddHiddenColumn("tcnIndex", Request.QueryString.Item("nIndexItin"))
        End With
	
        With mobjGridMerch
            
                With .Columns("valClass").Parameters
                    .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            If CStr(Session("sPolitype")) <> "1" Then
                With .Columns("valPacking").Parameters
                    .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nClassmerch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            End If
        End With
	
        '**+ The general properties of the grid are defined
        '+ Se definen las propiedades generales del grid
        With mobjGridMerch
            If mobjValues.StringToType(Request.QueryString.Item("nStageDet"), eFunctions.Values.eTypeData.etdInteger) > 0 Then
                .AddButton = True
            Else
                .AddButton = False
            End If
            .Codispl = Request.QueryString.Item("sCodispl")
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = .ActionQuery
            .Height = 400
            .Width = 300
            .Top = 55
            .Left = 170
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("valClass").EditRecord = True
            .Columns("valClass").Disabled = (Request.QueryString.Item("Action") <> "Add")
            .Columns("valPacking").Disabled = (Request.QueryString.Item("Action") <> "Add")
		
            .sEditRecordParam = "nInd=2" & "&nCurrency= ' + self.document.forms[0].cbeCurrency.value + '" & "&nStageDet=" & Request.QueryString.Item("nStageDet") & "&nIndexItin=" & Request.QueryString.Item("nIndexItin") & "&sStage_Merch= ' + self.document.forms[0].tctMerchandise.value + '"
            .sDelRecordParam = "nStagedet='+ marrArray[lintIndex].tcnMerchandise + '" & "&nInd=2" & "&nCurrency= ' + self.document.forms[0].cbeCurrency.value + '" & "&nClassmerch='+ marrArray[lintIndex].valClass + '" & "&nPacking=' + marrArray[lintIndex].valPacking + '"
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            .SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
        End With
    End Sub

    '%**Objetive: The controls of the page are load
    '%Objetivo: Se cargan los controles de la página
    '-------------------------------------------------------------------------------------------
    Private Sub insPreTR009_Itin()
        '-------------------------------------------------------------------------------------------
        Dim lcolTran_stages As ePolicy.Tran_stages
        Dim lclsTran_stage As Object
        Dim lintIndex As Short
	
        lcolTran_stages = New ePolicy.Tran_stages
        With mobjGridItin
		
		
            Response.Write("" & vbCrLf)
            Response.Write("	<TABLE WIDTH=""50%"">" & vbCrLf)
            Response.Write("		<TR>" & vbCrLf)
            Response.Write("			<TD><LABEL ID=9503>Moneda</LABEL></TD>" & vbCrLf)
            Response.Write(" 				")

            With mobjValues.Parameters
                .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With
            Response.Write("                  " & vbCrLf)
            Response.Write("			<TD>")

            mobjValues.BlankPosition = False
            Response.Write(mobjValues.PossiblesValues("cbeCurrency", "TabCurren_pol", 1, Request.QueryString.Item("nCurrency"), True, False, , , , "ReloadPage(this)", False, , vbNullString))
            Response.Write("</TD>" & vbCrLf)
            Response.Write("		</TR>        " & vbCrLf)
            Response.Write("	</TABLE>")

		
            If (mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdLong) = eRemoteDB.Constants.intNull) Then
                Response.Write("<script>ReloadPage(self.document.forms[0].cbeCurrency);</" & "Script>")
            End If
		
            If lcolTran_stages.Find(Session("sCertype"), _
                                    Session("nBranch"), _
                                    Session("nProduct"), _
                                    Session("nPolicy"), _
                                    Session("nCertif"), _
                                    Session("dEffecdate"), _
                                    mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdLong, True)) Then
                lintIndex = 0
                For Each lclsTran_stage In lcolTran_stages
                    .Columns("tcnStage").DefValue = lclsTran_stage.nStage
                    .Columns("tcdOrigindat").DefValue = lclsTran_stage.dOriginDat
                    .Columns("valRoute").DefValue = lclsTran_stage.nTyproute
                    .Columns("tcdDestindat").DefValue = lclsTran_stage.dDestindat
                    .Columns("tctName").DefValue = lclsTran_stage.sName_licen
                    .Columns("tcnCapital").DefValue = lclsTran_stage.nAmount
                    .Columns("tcnFrandedi").DefValue = lclsTran_stage.nFrandedi
                    .Columns("tctOrigen").DefValue = lclsTran_stage.sOrigen
                    .Columns("tctDestiny").DefValue = lclsTran_stage.sDestination
                    .Columns("tctPurchase_Order").DefValue = lclsTran_stage.sPurchase_Order
                    .Columns("tctApplicationNumber").DefValue = lclsTran_stage.sApplicationNumber
          
				
                    If CStr(Session("sPolitype")) <> "1" Then
                        .Columns("valTransport").DefValue = lclsTran_stage.nRoute
                        .Columns("valTransport").Parameters.Add("nTyproute", lclsTran_stage.nTyproute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Else
                        .Columns("valTransport").DefValue = lclsTran_stage.nTransptype
                    End If
				
                    .Columns("Sel").OnClick = "insdefMerch(this);"
                    .sEditRecordParam = "nInd=1&nTyproute=" + lclsTran_stage.nTyproute + "&nCurrency= ' + self.document.forms[0].cbeCurrency.value + '" + "&nStage='+ marrArrayItin[" & lintIndex & "].tcnStage + '"
				
                    If mobjValues.StringToType(Request.QueryString.Item("nIndexItin"), eFunctions.Values.eTypeData.etdLong) = lintIndex Then
                        .Columns("Sel").Checked = 1
                    Else
                        .Columns("Sel").Checked = 0
                    End If
                    lintIndex = lintIndex + 1
                    Response.Write(.DoRow)
                Next lclsTran_stage
            End If
        End With
        Response.Write(mobjGridItin.closeTable)
	
        lclsTran_stage = Nothing
        lcolTran_stages = Nothing
    End Sub

    '%**Objetive: The controls of the page are load
    '%Objetivo: Se cargan los controles de la página
    '-------------------------------------------------------------------------------------------
    Private Sub insPreTR009_Merch()
        '-------------------------------------------------------------------------------------------
        Dim lcolTran_stagedets As ePolicy.Tran_stagedets
        Dim lclsTran_stagedet As Object
        Dim lintIndex As Short
	
        lcolTran_stagedets = New ePolicy.Tran_stagedets
	
        Response.Write("" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("	<TABLE WIDTH=""70%"">" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<BR></BR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=9504>Mercancía de la etapa</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.TextControl("tctMerchandise", 30, Request.QueryString.Item("sStage_Merch") & Request.QueryString.Item("sOrigen") & Request.QueryString.Item("sDestination"), True, "Etapa a la que se le asocia la mercancía", , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            ")

        With mobjValues.Parameters
            .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        End With
        Response.Write(" " & vbCrLf)
        Response.Write("            " & vbCrLf)
        Response.Write("		</TR>        " & vbCrLf)
        Response.Write("	</TABLE>")

	
	
        With mobjGridMerch
            If lcolTran_stagedets.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.QueryString.Item("nStagedet"), eFunctions.Values.eTypeData.etdLong), Session("dEffecdate")) Then
                lintIndex = 0
                For Each lclsTran_stagedet In lcolTran_stagedets
                    .Columns("valClass").DefValue = lclsTran_stagedet.nClassmerch
                    .Columns("valClass").Descript = lclsTran_stagedet.sClassdesc
                    .Columns("valPacking").DefValue = lclsTran_stagedet.nPacking
                    .Columns("valPacking").Descript = lclsTran_stagedet.sPackdesc
                    .Columns("tcnQuantran").DefValue = lclsTran_stagedet.nQuantrans
                    .Columns("cbeUnit").DefValue = lclsTran_stagedet.nUnit
                    .Columns("tcnMerchrate").DefValue = lclsTran_stagedet.nMerchrate
                    .Columns("tcnCostUnit").DefValue = lclsTran_stagedet.nUnitvalue
                    .Columns("tcnCapital").DefValue = lclsTran_stagedet.nAmount
                    .Columns("tcnFrandedi").DefValue = lclsTran_stagedet.nFrandedi
                    .Columns("btnNoteNum").nNotenum = lclsTran_stagedet.nNotenum
                    .Columns("tcnImagenum").DefValue = lclsTran_stagedet.nImagenum
                    lintIndex = lintIndex + 1
                    Response.Write(.DoRow)
                Next lclsTran_stagedet
            End If
        End With
        Response.Write(mobjGridMerch.closeTable)
	
        lclsTran_stagedet = Nothing
        lcolTran_stagedets = Nothing
    End Sub

    '**%Objetive: The fields of the PopUp are defined
    '%Objetivo: Se definen los campos de la PopUp del detalle
    '-------------------------------------------------------------------------------------------
    Private Sub insPreTR009_ItinUpd()
        '-------------------------------------------------------------------------------------------
        Dim lclsTran_stage As ePolicy.Tran_stage
        Dim lclstran_route As ePolicy.tran_route
        Dim lclsErrors As eFunctions.Errors
	
        lclsTran_stage = New ePolicy.Tran_stage
        lclsErrors = New eFunctions.Errors
        lclsErrors.Highlighted = True
	
        With Request
            If .QueryString.Item("Action") = "Del" Then
                If lclsTran_stage.IsExistTran_stagedet(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), CInt(.QueryString.Item("nStage")), CInt(.QueryString.Item("nCurrency"))) Then
                    Response.Write(lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 90244, , , , True)) ' TODO: ojo con el parmetro aca, estaban otros
                Else
                    If CStr(Session("sPolitype")) <> "1" Then
                        If lclsTran_stage.InsPostTR009_Itin(.QueryString.Item("Action"), Session("sPolitype"), Session("nUsercode"), CInt(.QueryString.Item("nCurrency")), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), CInt(.QueryString.Item("nStage")), Session("dEffecdate"), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, vbNullString, vbNullString, vbNullString,vbNullString,vbNullString ) Then
                        End If
                    Else
                        If lclsTran_stage.InsPostTR009_Itin(.QueryString.Item("Action"), Session("sPolitype"), Session("nUsercode"), CInt(.QueryString.Item("nCurrency")), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), CInt(.QueryString.Item("nStage")), Session("dEffecdate"), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, vbNullString, vbNullString, vbNullString,vbNullString,vbNullString) Then
						
                            lclstran_route = New ePolicy.tran_route
                            Call lclstran_route.InsPostTR002(False, .QueryString.Item("sCodispl"), eRemoteDB.Constants.intNull, .QueryString.Item("Action"), Session("nUsercode"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nStage"), eFunctions.Values.eTypeData.etdLong), 0, 0, 0)
                            lclstran_route = Nothing
                            Response.Write(mobjValues.ConfirmDelete())
                        End If
                    End If
                    lclsTran_stage = Nothing
				
                End If
            End If
            Response.Write(mobjGridItin.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
        End With
        lclsErrors = Nothing
    End Sub

    '**%Objetive: The fields of the PopUp are defined
    '%Objetivo: Se definen los campos de la PopUp del detalle
    '-------------------------------------------------------------------------------------------
    Private Sub insPreTR009_MerchUpd()
        '-------------------------------------------------------------------------------------------
        Dim lclsTran_stagedet As ePolicy.Tran_stagedet
	
        With Request
            If .QueryString.Item("Action") = "Del" Then
                lclsTran_stagedet = New ePolicy.Tran_stagedet
			
                If lclsTran_stagedet.InsPostTR009_Merch(.QueryString.Item("Action"), Session("nUsercode"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), CInt(.QueryString.Item("nStageDet")), Session("dEffecdate"), CInt(Request.QueryString.Item("nClassmerch")), CInt(Request.QueryString.Item("nPacking")), CInt(Request.QueryString.Item("nCurrency")), 0, 0, 0, 0, 0, 0, 0, 0) Then
                    Response.Write(mobjValues.ConfirmDelete())
                End If
                lclsTran_stagedet = Nothing
            End If
		
            Response.Write(mobjGridMerch.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		
		
            If Request.QueryString.Item("Action") = "Update" Then
                Response.Write("<script>self.document.forms[0].tcnNotenum.value = top.opener.marrArray[CurrentIndex].btnNotenum;</" & "Script>")
            End If
		
        End With
    End Sub

</script>
<%
    '----------------------------------------------------------------------------------------------------
    '**+Objective: The Itineraries with it transported merchandise is indicated
    '**+Version: $$Revision: 4 $
    '+Objetivo: Indicar el itinerario y la mercancía transportada
    '+Version: $$Revision: 4 $
    '----------------------------------------------------------------------------------------------------
    Response.Expires = -1441

    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))


    mobjValues = New eFunctions.Values

    mobjValues.sSessionID = Session.SessionID
    mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")

%>
<html>
<head>
    <meta name="GENERATOR" content="Visual TIME Templates">
    <%=mobjValues.StyleSheet()%>
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script>
//%insParameter: Actualiza parametros del embalaje
//---------------------------------------------------------------------------
function insParameter(Field){
//---------------------------------------------------------------------------
	var lintCurrency
	with(self.document.forms[0]){
		if(typeof(valPacking)!='undefined'){
			if ("<%=Request.QueryString.Item("Action")%>" == "Add") {
				valPacking.disabled = false;
				btnvalPacking.disabled=false;
			}
			
			lintCurrency = "<%=Request.QueryString.Item("nCurrency")%>";
			valPacking.Parameters.Param1.sValue="<%=Session("sCertype")%>";
			valPacking.Parameters.Param2.sValue=<%=Session("nBranch")%>;
			valPacking.Parameters.Param3.sValue=<%=Session("nProduct")%>;
			valPacking.Parameters.Param4.sValue=<%=Session("nPolicy")%>;
			valPacking.Parameters.Param5.sValue=0;
			valPacking.Parameters.Param6.sValue=Field.value;
			valPacking.Parameters.Param7.sValue="<%=Session("dEffecdate")%>";
		
			if (Field.value == ''){
				valPacking.disabled = true;
				btnvalPacking.disabled=true;
				valPacking.value = '';
				tcnMerchRate.value= '';
				tcnFrandedi.value= '';
				UpdateDiv('valPackingDesc','');
			}
		}
	}
}	


//%insParameterInd: Actualiza parametros del embalaje
//---------------------------------------------------------------------------
function insParameterInd(Field){
//---------------------------------------------------------------------------
	var lintCurrency
	with(self.document.forms[0]){
		if(typeof(valPacking)!='undefined'){
			if ("<%=Request.QueryString.Item("Action")%>" == "Add") {
				valPacking.disabled = false;
				btnvalPacking.disabled = false;
			}
			
			lintCurrency = "<%=Request.QueryString.Item("nCurrency")%>";
			//valPacking.Parameters.Param1.sValue="<%=Session("sCertype")%>";
			//valPacking.Parameters.Param2.sValue=<%=Session("nBranch")%>;
			//valPacking.Parameters.Param3.sValue=<%=Session("nProduct")%>;
			//valPacking.Parameters.Param4.sValue=<%=Session("nPolicy")%>;
			//valPacking.Parameters.Param5.sValue=0;
			//valPacking.Parameters.Param6.sValue=Field.value;
			//valPacking.Parameters.Param7.sValue="<%=Session("dEffecdate")%>";
		
			if (Field.value == ''){
				valPacking.disabled = true;
				btnvalPacking.disabled = true;
				valPacking.value = '';
				tcnMerchRate.value= '';
				tcnFrandedi.value= '';
				UpdateDiv('valPackingDesc','');
			}
		}
	}
}	


//%insChargevalues: Carga los valores por defecto de la página
//---------------------------------------------------------------------------
function insChargevalues(){
//---------------------------------------------------------------------------
	with(self.document.forms[0]){
		if ((valClass.value > 0) && (valPacking.value > 0)){
			insDefValues('Tran_stagedet', 'nClassmerch=' + valClass.value + '&nPacking=' + valPacking.value + '&nCapital=' + tcnCapital.value + '&nQuantran=' + tcnQuantran.value + '&nCostUnit=' + tcnCostUnit.value, '/VTimeNet/Policy/PolicySeq');			
		}
	}
}	

//**% insShowImages: This function call the images window
//% insShowImages: Se invoca la ventana de imagenes
//-------------------------------------------------------------------------------------------
function insShowImages(nType, nImageNum, sClient, nCrThecni, blnQuery){
//-------------------------------------------------------------------------------------------
	var lintMainAction
	
    lintMainAction = (blnQuery?401:302)
    
    if (nType==1){
        with(self.document.forms[0]){
		    ShowPopUp('/VTimeNet/Common/SCA010.aspx?sCodispl=SCA10-7&nImageNum='+tcnImagenum.value+'&nMainAction='+lintMainAction+'&nRectype=5&WindowType=PopUp','PolicySeq',750,500,'no','no',20,20)
	    }
	}
	else
		ShowPopUp('/VTimeNet/Common/SCA010.aspx?sCodispl=SCA10-7&nImageNum='+nImageNum+'&nMainAction='+lintMainAction+'&nRectype=5&WindowType=PopUp','PolicySeq',750,500,'no','no',20,20)
}

//-------------------------------------------------------------------------------------------
function ReloadPage(Field){
//-------------------------------------------------------------------------------------------
	with(document.location){
		href = href.replace(/&nCurrency.*/,'') + '&nCurrency=' + Field.value
	}
}
//-------------------------------------------------------------------------------------------
function insdefMerch(Field){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if ((Field.checked)&&((marrArrayItin[Field.value].valRoute) > 0)){
			tctMerchandise.value = 'Etapa ' + marrArrayItin[Field.value].tcnStage + ': ' + marrArrayItin[Field.value].tctOrigen + ' - ' + marrArrayItin[Field.value].tctDestiny;
			with(document.location){
				href = href.replace(/&nStageDet.*/,'') + '&nStageDet=' + marrArrayItin[Field.value].tcnStage + '&nIndexItin=' + Field.value + '&sStage_Merch=' + tctMerchandise.value + '&sMerchandise=' + marrArrayItin[Field.value].valRoute;
			}
		}
		else{
			tctMerchandise.value = '';
			with(document.location){
				href = href.replace(/&nStageDet.*/,'') + '&nStageDet=0';
			}
		}
	}
}

//-------------------------------------------------------------------------------------------
function insCalValue(){
//-------------------------------------------------------------------------------------------
	var ldblCapital;
    var str;
	with(self.document.forms[0]){
		if((tcnQuantran.value!='')&&(tcnCostUnit.value!='')){
			ldblCapital = insConvertNumber(tcnQuantran.value)*insConvertNumber(tcnCostUnit.value);
            tcnCapital.value = ldblCapital.toString().replace(".",",");
			$(tcnCapital).change();
		}				  
		insChargevalues();
	}	    
}

    </script>
    <%
        With Request
            mobjValues.ActionQuery = (CDbl(.QueryString.Item("nMainAction")) = 401)
	
            If .QueryString.Item("Type") <> "PopUp" Then
                mobjMenues = New eFunctions.Menues
                mobjMenues.sSessionID = Session.SessionID
		
                Response.Write(mobjMenues.setZone(2, .QueryString.Item("sCodispl"), .QueryString.Item("sWindowDescript"), CShort(.QueryString.Item("nWindowTy"))))
		
                Response.Write("<script>var nMainAction = top.frames['fraSequence'].plngMainAction</script>")
		
                mobjMenues = Nothing
            End If
        End With
    %>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="<%=Request.QueryString.Item("sCodispl")%>" action="valPolicyseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&nCurrency=<%=Request.QueryString.Item("nCurrency")%>&sStage_Merch=<%=Request.QueryString.Item("sStage_Merch")%>">
    <%
        Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))

        insDefineHeader_Itin()
        insDefineHeader_Merch()

        If Request.QueryString.Item("Type") <> "PopUp" Then
            insPreTR009_Itin()
            insPreTR009_Merch()
        Else
            If CDbl(Request.QueryString.Item("nInd")) = 1 Then
                insPreTR009_ItinUpd()
            ElseIf CDbl(Request.QueryString.Item("nInd")) = 2 Then
                insPreTR009_MerchUpd()
                If CStr(Session("sPolitype")) <> "1" Then
                    Response.Write("<script>insParameter(self.document.forms[0].valClass);</script>")
                Else
                    Response.Write("<script>insParameterInd(self.document.forms[0].valClass);</script>")
                End If
            End If
        End If

        mobjGridItin = Nothing
        mobjGridMerch = Nothing
        mobjValues = Nothing

        mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
        mobjNetFrameWork = Nothing
    %>
    </form>
</body>
</html>
<script>
    //% changevaluesField: se controla el cambio de valor de los campos de la ventana
    //--------------------------------------------------------------------------------------------
    function ShowName_Licen() {
        //--------------------------------------------------------------------------------------------
        ShowPopUp('/VTimeNet/Common/popup.aspx?sPagename=/VTimeNet/Branches/BranchQue/TRC6000_K&sCodispl=TRC6000', 'Consulta', 600, 400, 'yes', 'no', 250, 150);
    }
    //-------------------------------------------------------------------------------------------
    function insParamValues(Field) {
        //-------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            valTransport.Parameters.Param7.sValue = Field.value;
            if (Field.value != '') {
                valTransport.disabled = false;
                btnvalTransport.disabled = false;
            }
            else {
                valTransport.disabled = true;
                btnvalTransport.disabled = true;
                UpdateDiv('valTransportDesc', '');
                valTransport.value = '';
            }
            tctName.disabled = (Field.value == '1') ? true : false;
            btnName.disabled = (Field.value != '1' && Field.value != '4') ? true : false;
        }
    }

    //-------------------------------------------------------------------------------------------
    function insParamValuesInd(Field) {
        //-------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            if (Field.value != '') {
                valTransport.disabled = false;
                btnvalTransport.disabled = false;
            }
            else {
                valTransport.disabled = true;
                btnvalTransport.disabled = true;
                UpdateDiv('valTransportDesc', '');
                valTransport.value = '';
            }
            tctName.disabled = (Field.value == '1' ? true : false);
            btnName.disabled = (Field.value != '1' && Field.value != '4') ? true : false;
        }
    }
</script>
