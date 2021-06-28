<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import Namespace="System.Globalization" %>
<%@ Import namespace="eFunctions" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="eFunctions.Values" %>
<%@ Import namespace="eRemoteDB.Parameter" %>

<script language="VB" runat="Server">

	'- Objeto para el manejo de las funciones generales de carga de valores
	Private mobjValues As New eFunctions.Values
	
	'- Se define la variable mobjGrid para el manejo del Grid de la ventana
	Private mobjGrid As eFunctions.Grid

    ''' <summary>
    ''' Definición de columnas del GRID
    ''' </summary>
	Private Sub insDefineHeader()
	    mobjGrid = New eFunctions.Grid
			
        '+Se definen todas las columnas del Grid
        With mobjGrid.Columns
            If Request.QueryString.Item("Type") = "PopUp" Then
                .AddPossiblesColumn(Id:=0, Title:=GetLocalResourceObject("NMODULEC_Caption"), FieldName:="NMODULEC", TableName:="TABTAB_MODUL", ValuesType:=eValuesType.clngWindowType, DefValue:="", NeedParam:=True, ComboSize:="1", OnChange:="InputOnChange(this)", Disabled:=False, MaxLength:=0, Alias_Renamed:=GetLocalResourceObject("NMODULEC_ToolTip"), CodeType:=eTypeCode.eNumeric, bAllowInvalid:=False, ShowDescript:=True, Descript:="", NotCache:=False, KeyField:="")
                .AddPossiblesColumn(Id:=0, Title:=GetLocalResourceObject("NCOVER_Caption"), FieldName:="NCOVER", TableName:="TAB_COVER", ValuesType:=eValuesType.clngWindowType, DefValue:="", NeedParam:=True, ComboSize:="1", OnChange:="InputOnChange(this)", Disabled:=False, MaxLength:=0, Alias_Renamed:=GetLocalResourceObject("NCOVER_ToolTip"), CodeType:=eTypeCode.eNumeric, bAllowInvalid:=False, ShowDescript:=True, Descript:="", NotCache:=False, KeyField:="")
                .AddPossiblesColumn(0, GetLocalResourceObject("valAgreementColumnCaption"), "valAgreement", "tabAgreement_al", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , Request.QueryString.Item("Action") = "Update", , GetLocalResourceObject("valAgreementColumnToolTip"))
                .AddPossiblesColumn(Id:=0, Title:=GetLocalResourceObject("NVEHTYPE_Caption"), FieldName:="NVEHTYPE", TableName:="TABLE78109", ValuesType:=eValuesType.clngComboType, DefValue:="", NeedParam:=False, ComboSize:="1", OnChange:="InputOnChange(this)", Disabled:=False, MaxLength:=5, Alias_Renamed:=GetLocalResourceObject("NVEHTYPE_ToolTip"), CodeType:=eTypeCode.eNumeric, bAllowInvalid:=False, ShowDescript:=True, Descript:="", NotCache:=False, KeyField:="")

            Else
                .AddTextColumn(Id:=0, Title:=GetLocalResourceObject("NMODULEC_Caption"), FieldName:="tctModulec", Length:=30, DefValue:="")
                .AddTextColumn(Id:=0, Title:=GetLocalResourceObject("NCOVER_Caption"), FieldName:="tctCover", Length:=120, DefValue:="")
                .AddTextColumn(Id:=0, Title:=GetLocalResourceObject("valAgreementColumnCaption"), FieldName:="tctAgreement", Length:=30, DefValue:="")
                .AddTextColumn(Id:=0, Title:=GetLocalResourceObject("NVEHTYPE_Caption"), FieldName:="tctVehtype", Length:=30, DefValue:="")
                
                .AddHiddenColumn(FieldName:="NMODULEC", DefValue:=CStr(0))
                .AddHiddenColumn(FieldName:="NCOVER", DefValue:=CStr(0))
                .AddHiddenColumn(FieldName:="valAgreement", DefValue:=CStr(0))
                .AddHiddenColumn(FieldName:="NVEHTYPE", DefValue:=CStr(0))
            End If
                 
            .AddNumericColumn(Id:=0, Title:=GetLocalResourceObject("NPREMIUM_Caption"), FieldName:="NPREMIUM", Length:=18, DefValue:="", isRequired:=True, Alias_Renamed:=GetLocalResourceObject("NPREMIUM_ToolTip"), ShowThousand:=False, DecimalPlaces:=6, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddHiddenColumn(FieldName:="dEffecdateCurrent", DefValue:="""")
                
        End With
        
	    With mobjGrid
			.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = "MSO009"
			.Codisp = "MSO009"
			.Top = 100
			.Height = 288
			.Width = 550
			.ActionQuery = mobjValues.ActionQuery
			.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
			.Columns("Sel").GridVisible = Not .ActionQuery
		            
            If Request.QueryString.Item("Type") <> "PopUp" Then            
                .Columns("tctModulec").EditRecord = True
            End If
            
		    If Request.QueryString.Item("Type") = "PopUp" Then
		        .Columns("NMODULEC").Disabled = (Request.QueryString.Item("Action") = "Update")
			    .Columns("NCOVER").Disabled = (Request.QueryString.Item("Action") = "Update")
                .Columns("NVEHTYPE").Disabled = (Request.QueryString.Item("Action") = "Update")
			End If
                    
            .sDelRecordParam = "cbeBranch=" & Request.QueryString.Item("cbeBranch") + "&valProduct=" & Request.QueryString.Item("valProduct") + "&NCURRENCY=" & Request.QueryString.Item("NCURRENCY") + "&DEFFECDATE=" & Request.QueryString.Item("DEFFECDATE") & "&NMODULEC=' + marrArray[lintIndex].NMODULEC + '" & "&NCOVER=' + marrArray[lintIndex].NCOVER + '" & "&NVEHTYPE=' + marrArray[lintIndex].NVEHTYPE + '" & "&dEffecdateCurrent=' + marrArray[lintIndex].dEffecdateCurrent + '" & "&nAgreement=' + marrArray[lintIndex].valAgreement + '"
            .sEditRecordParam = "cbeBranch=" & Request.QueryString.Item("cbeBranch") + "&valProduct=" & Request.QueryString.Item("valProduct") + "&NCURRENCY=" & Request.QueryString.Item("NCURRENCY") + "&DEFFECDATE=" & Request.QueryString.Item("DEFFECDATE")
					
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
					
        	.AddButton = True
        	.DeleteButton = True
            .Columns("Sel").GridVisible = .DeleteButton
            

            .Columns("valAgreement").Parameters.Add("sStatregt", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("NMODULEC").Parameters.Add("NBRANCH", mobjValues.StringToType(Request.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
            .Columns("NMODULEC").Parameters.Add("NPRODUCT", mobjValues.StringToType(Request.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
            .Columns("NMODULEC").Parameters.Add("DEFFECDATE", mobjValues.StringToType(Request.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
            .Columns("NCOVER").Parameters.Add("NBRANCH", mobjValues.StringToType(Request.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
            .Columns("NCOVER").Parameters.Add("NPRODUCT", mobjValues.StringToType(Request.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
            .Columns("NCOVER").Parameters.Add("NMODULEC", 0, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
            .Columns("NCOVER").Parameters.Add("DEFFECDATE", mobjValues.StringToType(Request.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)


        End With
	End Sub

    ''' <summary>
    ''' Esta función se encarga de cargar los datos en la forma "Folder" 
    ''' </summary>
		Private Sub insPreMSO009()
       With Request                    
          Dim rdb As New eRemoteDB.Execute
            
            rdb.SQL = "SELECT /*+ INDEX (TAR_SOAP XPKTAR_SOAP2) */ TAR_SOAP.NMODULEC, TAR_SOAP.NCOVER, TAR_SOAP.NVEHTYPE, TAR_SOAP.NPREMIUM, TAR_SOAP.DNULLDATE, TAR_SOAP.NBRANCH, TAR_SOAP.NPRODUCT, TAR_SOAP.NCURRENCY, TAR_SOAP.DEFFECDATE ,REAGENERALPKG.REASCOVER(NULL,NULL,TAR_SOAP.NBRANCH, TAR_SOAP.NPRODUCT, TAR_SOAP.NMODULEC, TAR_SOAP.NCOVER, :DEFFECDATE1) AS SCOVER, REAGENERALPKG.REASMODULEC(TAR_SOAP.NBRANCH, TAR_SOAP.NPRODUCT, TAR_SOAP.NMODULEC, :DEFFECDATE2) AS SMODULEC, TABLE78109.SDESCRIPT AS SVEHTYPE, TAR_SOAP.NAGREEMENT, AL.SDESCRIPT AS SAGREEMENT  " &
                  "     FROM INSUDB.TAR_SOAP TAR_SOAP  " &
      "   INNER JOIN INSUDB.TABLE78109  " &
      "	     ON TABLE78109.NSOAP_VEHTYPE = TAR_SOAP.NVEHTYPE  " &
      "   LEFT JOIN AGREEMENT_AL AL  " &
      "	     ON AL.NAGREEMENT = TAR_SOAP.NAGREEMENT  " &
      "     AND AL.SSTATREGT  = '1' " &
      "     AND AL.DSTARTDATE <= :DEFFECDATE3 AND ( AL.DNULLDATE IS NULL OR AL.DNULLDATE > :DNULLDATE1 )" &
          "		   WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NCURRENCY = :NCURRENCY AND DEFFECDATE <= :DEFFECDATE AND  ( TAR_SOAP.DNULLDATE IS NULL OR TAR_SOAP.DNULLDATE > :DNULLDATE0 )   " &
         "		   ORDER BY TAR_SOAP.NMODULEC,TAR_SOAP.NCOVER,TAR_SOAP.NAGREEMENT,TABLE78109.SDESCRIPT "
            
          rdb.Parameters.Add("DEFFECDATE1", mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
          rdb.Parameters.Add("DEFFECDATE2", mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
          rdb.Parameters.Add("DEFFECDATE3", mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
          rdb.Parameters.Add("DNULLDATE1", mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)              
          rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
          rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
          rdb.Parameters.Add("NCURRENCY", mobjValues.StringToType(.QueryString.Item("NCURRENCY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
          rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
          rdb.Parameters.Add("DNULLDATE0", mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)

          With mobjGrid
            If rdb.Run(True) Then
               Do While Not rdb.EOF
                    
                    .Columns("NMODULEC").Parameters.Add("NBRANCH", mobjValues.StringToType(Request.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong))
                    .Columns("NMODULEC").Parameters.Add("NPRODUCT", mobjValues.StringToType(Request.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong))
                    .Columns("NMODULEC").Parameters.Add("DEFFECDATE", mobjValues.StringToType(Request.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate))
                    .Columns("NMODULEC").DefValue = CStr(rdb.FieldToClass("NMODULEC"))
                    .Columns("NMODULEC").Descript = rdb.FieldToClass("SMODULEC")
                    .Columns("NCOVER").Parameters.Add("NBRANCH", mobjValues.StringToType(Request.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong))
                    .Columns("NCOVER").Parameters.Add("NPRODUCT", mobjValues.StringToType(Request.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong))
                    .Columns("NCOVER").Parameters.Add("NMODULEC", .Columns("NMODULEC").DefValue)
                    .Columns("NCOVER").Parameters.Add("DEFFECDATE", mobjValues.StringToType(Request.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate))
                    .Columns("NCOVER").DefValue = CStr(rdb.FieldToClass("NCOVER"))
                    .Columns("NCOVER").Descript = rdb.FieldToClass("SCOVER")
                    .Columns("NVEHTYPE").DefValue = CStr(rdb.FieldToClass("NVEHTYPE"))
                    .Columns("NVEHTYPE").Descript = rdb.FieldToClass("SVEHTYPE")                       
                    .Columns("valAgreement").Parameters.Add("sStatregt", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Columns("valAgreement").DefValue = CStr(rdb.FieldToClass("nAgreement"))
                    
                    .Columns("tctModulec").DefValue = rdb.FieldToClass("sModulec")
                    .Columns("tctCover").DefValue = rdb.FieldToClass("sCover")
                    .Columns("tctAgreement").DefValue = rdb.FieldToClass("sAgreement")
                    .Columns("tctVehtype").DefValue = rdb.FieldToClass("sVehtype")    
                    .Columns("NPREMIUM").DefValue = rdb.FieldToClass("NPREMIUM")
                    .Columns("dEffecdateCurrent").DefValue = rdb.FieldToClass("dEffecdate")

                 Response.Write(.DoRow)
                 rdb.RNext()
               Loop                 
               rdb.RCloseRec()              
            End If            
            Response.Write(.CloseTable())        
          End With
       End With		
	  End Sub

    ''' <summary>
    ''' Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
    ''' </summary>
		Private Sub insPreMSO009Upd()
				With Request
					If .QueryString.Item("Action") = "Del" Then						
               	Dim lblnPost As Boolean
                Dim lstrMessage As String = String.Empty
               	Dim rdb As New eRemoteDB.Execute
                
                
                 If String.IsNullOrEmpty(lstrMessage) Then
                             If mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate) = mobjValues.StringToType(.QueryString.Item("dEffecdateCurrent"), eFunctions.Values.eTypeData.etdDate) Then 

 rdb = New eRemoteDB.Execute 
                        rdb.SQL = "DELETE FROM INSUDB.TAR_SOAP WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NCURRENCY = :NCURRENCY AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND NVEHTYPE = :NVEHTYPE AND NAGREEMENT = :NAGREEMENT AND DEFFECDATE = :DEFFECDATE "
    
 rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCURRENCY", mobjValues.StringToType(.QueryString.Item("NCURRENCY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(.QueryString.Item("NMODULEC"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.QueryString.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NVEHTYPE", mobjValues.StringToType(.QueryString.Item("NVEHTYPE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NAGREEMENT", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("nAgreement")), 0, .QueryString.Item("nAgreement")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)

                                       
          lblnPost = rdb.Run(False)
         Else 

 rdb = New eRemoteDB.Execute 
          rdb.SQL = "UPDATE INSUDB.TAR_SOAP SET DNULLDATE = :DNULLDATE, NUSERCODE = :NUSERCODE, DCOMPDATE = SYSDATE WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NCURRENCY = :NCURRENCY AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND NVEHTYPE = :NVEHTYPE AND NAGREEMENT = :NAGREEMENT AND DEFFECDATE = :DEFFECDATE"
    
 rdb.Parameters.Add("DNULLDATE", mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NUSERCODE", Session("NUSERCODE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCURRENCY", mobjValues.StringToType(.QueryString.Item("NCURRENCY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(.QueryString.Item("NMODULEC"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.QueryString.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NVEHTYPE", mobjValues.StringToType(.QueryString.Item("NVEHTYPE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NAGREEMENT", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("nAgreement")), 0, .QueryString.Item("nAgreement")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("dEffecdateCurrent"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)

                                       
          lblnPost = rdb.Run(False)
        End if

                     Response.Write(mobjValues.ConfirmDelete())
                 Else
                     Response.Write(lstrMessage)
                 End If
		      End If
					
					Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "MSO009_val.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
				End With		
		End Sub

</script>
<%      
	Response.Expires = -1441
%>
<script type="text/javascript" language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<script type="text/javascript" language="JavaScript">
    function InputOnChange(field) {
        switch (field.name) {   
            case 'NMODULEC':
                if (field.value != "")
                    with (self.document.forms[0]) {
                        //NCOVER.Parameters.Param1.sValue = cbeBranch.value
                        //NCOVER.Parameters.Param2.sValue = valProduct.value
                        NCOVER.Parameters.Param3.sValue = NMODULEC.value
                        //NCOVER.Parameters.Param4.sValue = DEFFECDATE.value;

                    }
                break;
        
        }    
    }
</script>
<html>
<head>
    <title></title>
		<%
				mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
		    Response.Write(mobjValues.StyleSheet())
		    Response.Write("<script language='JavaScript'>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</script>")
		    If Request.QueryString.Item("Type") <> "PopUp" Then
		        With New eFunctions.Menues
		        		Response.Write(.setZone(2, "MSO009", "MSO009.aspx"))
		        End With
		    End If
		%>
</head>
<body onunload="closeWindows();">
    <form method="post" id="FORM" action="MSO009_val.aspx?sZone=2">
		<%
				Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
				Call insDefineHeader()
				If Request.QueryString.Item("Type") <> "PopUp" Then
				    Call insPreMSO009()
				Else
				    Call insPreMSO009Upd()
				End If
		%>	  
    </form>
</body>
</html>