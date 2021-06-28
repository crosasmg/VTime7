<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI757"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("MVI757", "MVI757_K.aspx", 1, vbNullString))
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%>

<SCRIPT> 
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"
</SCRIPT>        
<SCRIPT LANGUAGE=JavaScript>

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tcdEffecdate.disabled = false;
		btn_tcdEffecdate.disabled = false;
		cbeBranch.disabled = false;
	}
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción finalizar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//% ChangeValues: 
//--------------------------------------------------------------------------------------------
function ChangeValues(Field, Case){
//--------------------------------------------------------------------------------------------
	switch(Case){
		case "Branch":						
			with(self.document.forms[0]){
				StatePossibleValues("valProduct", Field);
				StatePossibleValues("valModulec", valProduct);
				valProduct.Parameters.Param1.sValue = Field.value;
				valModulec.Parameters.Param1.sValue = Field.value;
				valCover.Parameters.Param2.sValue = Field.value;
				tcdEffecdate.value="";
				valCover.value="";
				UpdateDiv('valCoverDesc','');				
			}
			break;

		case "Product":						
			with(self.document.forms[0]){
				if(Field.value=="")
					StatePossibleValues("valModulec", valProduct);
				else
					StatePossibleValues("valModulec", tcdEffecdate);				
				    valModulec.Parameters.Param2.sValue = Field.value;
				    valCover.Parameters.Param3.sValue = Field.value;
				    tcdEffecdate.value="";
				    valCover.value="";
				    UpdateDiv('valCoverDesc','');				    
			}
			break;

		case "Effecdate":								
			with(self.document.forms[0]){
				StatePossibleValues("valModulec", tcdEffecdate);
				valModulec.Parameters.Param3.sValue = Field.value;
				valCover.Parameters.Param5.sValue = Field.value;
				insDefValues('MVI757', 'nBranch=' + cbeBranch.value  + '&nProduct=' + valProduct.value + '&dEffecdate=' + tcdEffecdate.value, '/VTimeNet/Maintenance/MantLife');
			}
			break;

		case "Modulec":		
			with(self.document.forms[0]){
			    valCover.Parameters.Param4.sValue = Field.value;
			}
			break;
	}
			
	with (self.document.forms[0]){
        if (cbeBranch.value>0 && valProduct.value>0 &&
            tcdEffecdate.value !='' && valModulec.value !=''){
		    valCover.disabled = false;
            btnvalCover.disabled = false;
        }
        else{
		      valCover.disabled = true;            
              btnvalCover.disabled = true;
        }          
	}  
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="POST" NAME="MVI757" ACTION="valMantLife.aspx?sMode=2">
	<BR><BR>
	    <TABLE WIDTH="100%">
	        <TR>
	            <TD WIDTH=20% ><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
	            <TD WIDTH=30% ><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "ChangeValues(this, ""Branch"")", True,  , GetLocalResourceObject("cbeBranchToolTip"))%></TD>
				<TD WIDTH=15% ><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
				<TD><%With mobjValues
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "ChangeValues(this, ""Product"")", True, 4, GetLocalResourceObject("valProductToolTip")))
End With
%>
				</TD>
	        </TR>
			<TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
				<TD><%=mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "ChangeValues(this, ""Effecdate"")", True)%></TD>
				<TD><LABEL ID=0><%= GetLocalResourceObject("valModulecCaption") %></LABEL></TD>
				<TD><%With mobjValues
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "ChangeValues(this, ""Modulec"")", True, 4, GetLocalResourceObject("valModulecToolTip")))
End With
%>
				</TD>
			</TR>
	        <TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("valCoverCaption") %></LABEL></TD>			
				<TD COLSPAN="3" ><%With mobjValues
	.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nCovernoshow", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nCovermax", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valCover", "tablife_cover", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valCoverToolTip")))
End With
%>
				</TD>
	        </TR>
	        
	    </TABLE>
	</FORM>
<%
mobjValues = Nothing%>
</BODY>
</HTML>





