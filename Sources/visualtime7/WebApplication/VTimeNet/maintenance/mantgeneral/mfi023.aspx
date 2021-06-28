<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 11.58.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú de la página

Dim MobjMenu As eFunctions.Menues

'- Objeto para manejar las opciones de instalación

Dim mobjOptionInstall As eGeneral.OptFinance



'% insPreMFI023A : define la estructura de la página con las opciones de intalación  de la tabla opt_financ
'--------------------------------------------------------------------------------------------------
Private Function insPreMFI023A() As Object
	'--------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    	<TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><LABEL >" & GetLocalResourceObject("cbeOptDraCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%"">")


Response.Write(mobjValues.PossiblesValues("cbeOptDra", "Table252", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nOpt_draft),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  , GetLocalResourceObject("cbeOptDraToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%"">&nbsp;</TD>   " & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><LABEL>" & GetLocalResourceObject("cboOptNullCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%"">")


Response.Write(mobjValues.PossiblesValues("cboOptNull", "Table254", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nOpt_null),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboOptNullToolTip")))


Response.Write("</TD>        " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("    		<TD>")


Response.Write(mobjValues.CheckControl("chkSchOptDra", GetLocalResourceObject("chkSchOptDraCaption"), mobjOptionInstall.sCh_opt_dra))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>   " & vbCrLf)
Response.Write("    		<TD>")


Response.Write(mobjValues.CheckControl("chkSchOptNull", GetLocalResourceObject("chkSchOptNullCaption"), mobjOptionInstall.sCh_opt_nul))


Response.Write("</TD>" & vbCrLf)
Response.Write("    		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("             <TD NOWRAP><LABEL>" & GetLocalResourceObject("tcnLevelDraCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("             <TD>")


Response.Write(mobjValues.NumericControl("tcnLevelDra", 5, CStr(mobjOptionInstall.nLevel_dra),  , GetLocalResourceObject("tcnLevelDraToolTip"), False, False))


Response.Write("</TD>" & vbCrLf)
Response.Write("             <TD>&nbsp;</TD>   " & vbCrLf)
Response.Write("             <TD NOWRAP><LABEL>" & GetLocalResourceObject("tcnLevelDraCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("             <TD>")


Response.Write(mobjValues.NumericControl("tcnLevelNull", 5, CStr(mobjOptionInstall.nLevel_nul),  , GetLocalResourceObject("tcnLevelNullToolTip"), False, False,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    	<TR>" & vbCrLf)
Response.Write("    	    <TD COLSPAN=5>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>    " & vbCrLf)
Response.Write("    <!-------------------------------------------------Fin Generales---------------------------------------------------->" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("           <TD COLSPAN=""5"" CLASS=""HIGHLIGHTED""><LABEL><A NAME=""Financiamiento"">" & GetLocalResourceObject("AnchorFinanciamientoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("           <TD COLSPAN=""5"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("           <TD COLSPAN=""5""><BR></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("      " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("           <TD WIDTH=5%><LABEL>" & GetLocalResourceObject("tcnDefaultiCaption") & "</LABEL>&nbsp;")


Response.Write(mobjValues.NumericControl("tcnDefaulti", 4, CStr(mobjOptionInstall.nDefaulti),  , GetLocalResourceObject("tcnDefaultiToolTip"), False, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("           <TD WIDTH=20%>&nbsp;</TD>   " & vbCrLf)
Response.Write("           <TD WIDTH=37%>&nbsp;</TD>   " & vbCrLf)
Response.Write("    	   <TD WIDTH=5%>&nbsp;</TD>" & vbCrLf)
Response.Write("           <TD>&nbsp;</TD>   " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    	<TR>" & vbCrLf)
Response.Write("          <TD>")


Response.Write(mobjValues.CheckControl("chkSchUp", GetLocalResourceObject("chkSchUpCaption"), mobjOptionInstall.sCh_up,  , "Enabled(this, ""InterestUp"")",  ,  , GetLocalResourceObject("chkSchUpToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("          <TD WIDTH=5%><LABEL>" & GetLocalResourceObject("tcnDefaultiCaption") & "</LABEL>" & vbCrLf)
Response.Write("              ")


Response.Write(mobjValues.NumericControl("tcnIntUp", 6, CStr(mobjOptionInstall.nInt_up),  , GetLocalResourceObject("tcnIntUpToolTip"), False, 2,  ,  ,  ,  , True))


Response.Write("" & vbCrLf)
Response.Write("          </TD>" & vbCrLf)
Response.Write("          <TD>&nbsp;</TD>   " & vbCrLf)
Response.Write("          <TD NOWRAP>")


Response.Write(mobjValues.CheckControl("chkSoptIntere", GetLocalResourceObject("chkSoptIntereCaption"), mobjOptionInstall.sOpt_intere,  ,  ,  ,  , GetLocalResourceObject("chkSoptIntereToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("          <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("          </TR>" & vbCrLf)
Response.Write("     " & vbCrLf)
Response.Write("     	<TR>" & vbCrLf)
Response.Write("          	<TD>")


Response.Write(mobjValues.CheckControl("chkSchDown", GetLocalResourceObject("chkSchDownCaption"), mobjOptionInstall.sCh_down,  , "Enabled(this, ""InterestDown"")",  ,  , GetLocalResourceObject("chkSchDownToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("          	<TD WIDTH=5%><LABEL>" & GetLocalResourceObject("tcnDefaultiCaption") & "</LABEL>" & vbCrLf)
Response.Write("              ")


Response.Write(mobjValues.NumericControl("tcnIntDown", 6, CStr(mobjOptionInstall.nInt_down),  , GetLocalResourceObject("tcnIntDownToolTip"), False, 2,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("           <TD>&nbsp;</TD>      " & vbCrLf)
Response.Write("           <TD>")


Response.Write(mobjValues.CheckControl("chkSchOptIntere", GetLocalResourceObject("chkSchOptIntereCaption"), mobjOptionInstall.sCh_opt_int))


Response.Write("</TD>   " & vbCrLf)
Response.Write("           <TD>&nbsp;</TD>   " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("         " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("           <TD NOWRAP><LABEL>" & GetLocalResourceObject("tcnLevelDraCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("           <TD>&nbsp;&nbsp;&nbsp;&nbsp;")


Response.Write(mobjValues.NumericControl("tcnLevelFin", 5, CStr(mobjOptionInstall.nLevel_fin),  , GetLocalResourceObject("tcnLevelFinToolTip"), False, False))


Response.Write("</TD>" & vbCrLf)
Response.Write("           <TD>&nbsp;</TD>   " & vbCrLf)
Response.Write("           <TD NOWRAP><LABEL>&nbsp;" & GetLocalResourceObject("tcnLevelInitialCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("           <TD>")


Response.Write(mobjValues.NumericControl("tcnLevelInitial", 5, CStr(mobjOptionInstall.nLevel_initial),  , GetLocalResourceObject("tcnLevelInitialToolTip"), False, False))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    	<TR>" & vbCrLf)
Response.Write("    	   <TD COLSPAN=5>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <!------------------------------------FINIntereses de financiamiento-------------------------------------------------------->" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("   <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("    		<TD COLSPAN=2 CLASS=""HIGHLIGHTED""><LABEL>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    		<TD COLSPAN=2>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("    		<TD COLSPAN=2 CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("    		<TD COLSPAN=2></TD>" & vbCrLf)
Response.Write("        </TR>       " & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("        ")

	If mobjOptionInstall.sInterest_e = "1" And mobjOptionInstall.sTime_exa = "1" Then
Response.Write("     " & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("    		    <TD WIDTH=25%><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    		    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    		    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    		    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    		</TR>" & vbCrLf)
Response.Write("             <TR>" & vbCrLf)
Response.Write("                <TD WIDTH=25% ALIGN=CENTER>")


Response.Write(mobjValues.OptionControl(0, "optSInterestE", GetLocalResourceObject("optSInterestE_CStr1Caption"), CStr(1), CStr(1)))


Response.Write("</TD>" & vbCrLf)
Response.Write("                <TD WIDTH=25% ALIGN=LEFT>")


Response.Write(mobjValues.OptionControl(0, "optSInterestE", GetLocalResourceObject("optSInterestE_CStr2Caption"),  , CStr(2)))


Response.Write("</TD>" & vbCrLf)
Response.Write("    		    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    		    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("             </TR>" & vbCrLf)
Response.Write("             <TR>" & vbCrLf)
Response.Write("    			<TD WIDTH=25%><LABEL>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    		    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    		    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    		    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    		</TR>" & vbCrLf)
Response.Write("    	     <TR>" & vbCrLf)
Response.Write("    			<TD WIDTH=25% ALIGN=CENTER>")


Response.Write(mobjValues.OptionControl(0, "optSTimeExa", GetLocalResourceObject("optSTimeExa_CStr1Caption"), CStr(1), CStr(1)))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	        <TD WIDTH=25% ALIGN=LEFT>")


Response.Write(mobjValues.OptionControl(0, "optSTimeExa", GetLocalResourceObject("optSTimeExa_CStr2Caption"),  , CStr(2)))


Response.Write("</TD>" & vbCrLf)
Response.Write("    		    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    		    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("        ")

	Else
Response.Write("" & vbCrLf)
Response.Write("               ")

		If mobjOptionInstall.sInterest_e = "1" And mobjOptionInstall.sTime_exa = "2" Then
Response.Write("" & vbCrLf)
Response.Write("    				<TR>" & vbCrLf)
Response.Write("    				    <TD WIDTH=25%><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    				    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    				    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    				    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    				</TR> " & vbCrLf)
Response.Write("    				<TR>" & vbCrLf)
Response.Write("    					<TD WIDTH=25% ALIGN=CENTER>")


Response.Write(mobjValues.OptionControl(0, "optSInterestE", GetLocalResourceObject("optSInterestE_1Caption"), CStr(1), "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    					<TD WIDTH=25% ALIGN=LEFT>")


Response.Write(mobjValues.OptionControl(0, "optSInterestE", GetLocalResourceObject("optSInterestE_2Caption"),  , "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    					<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    					<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    				</TR>" & vbCrLf)
Response.Write("    				<TR>" & vbCrLf)
Response.Write("    					<TD WIDTH=25%><LABEL>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    					<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    					<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    					<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    				</TR>   " & vbCrLf)
Response.Write("    				<TR> " & vbCrLf)
Response.Write("    				    <TD WIDTH=25% ALIGN=CENTER>")


Response.Write(mobjValues.OptionControl(0, "optSTimeExa", GetLocalResourceObject("optSTimeExa_1Caption"),  , "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	                <TD WIDTH=25% ALIGN=LEFT>")


Response.Write(mobjValues.OptionControl(0, "optSTimeExa", GetLocalResourceObject("optSTimeExa_2Caption"), CStr(1), "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    					<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    					<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    				</TR>" & vbCrLf)
Response.Write("               ")

		Else
Response.Write(" " & vbCrLf)
Response.Write("    		        ")

			If mobjOptionInstall.sInterest_e = "2" And mobjOptionInstall.sTime_exa = "1" Then
Response.Write("" & vbCrLf)
Response.Write("                        <TR>" & vbCrLf)
Response.Write("    					    <TD WIDTH=25%><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    					    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    					    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    					    <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    					</TR>" & vbCrLf)
Response.Write("                         <TR>" & vbCrLf)
Response.Write("    						<TD WIDTH=25% ALIGN=CENTER>")


Response.Write(mobjValues.OptionControl(0, "optSInterestE", GetLocalResourceObject("optSInterestE_1Caption"),  , "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    						<TD WIDTH=25% ALIGN=LEFT>")


Response.Write(mobjValues.OptionControl(0, "optSInterestE", GetLocalResourceObject("optSInterestE_2Caption"), CStr(1), "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    						<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    						<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("                        </TR>" & vbCrLf)
Response.Write("                        <TR>" & vbCrLf)
Response.Write("    						<TD WIDTH=25%><LABEL>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    						<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    						<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    						<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    					</TR>" & vbCrLf)
Response.Write("                        <TR>" & vbCrLf)
Response.Write("    					    <TD WIDTH=25% ALIGN=CENTER>")


Response.Write(mobjValues.OptionControl(0, "optSTimeExa", GetLocalResourceObject("optSTimeExa_1Caption"), CStr(1), "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	                    <TD WIDTH=25% ALIGN=LEFT>")


Response.Write(mobjValues.OptionControl(0, "optSTimeExa", GetLocalResourceObject("optSTimeExa_2Caption"),  , "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    						<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    						<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("                       </TR>" & vbCrLf)
Response.Write("                    ")

			Else
				If mobjOptionInstall.sInterest_e = "" And mobjOptionInstall.sTime_exa = "" Then
Response.Write("" & vbCrLf)
Response.Write("    			 				<TR>" & vbCrLf)
Response.Write("    								<TD WIDTH=25%><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    								<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    								<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    								<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    							</TR> " & vbCrLf)
Response.Write("                                <TR>" & vbCrLf)
Response.Write("                                    <TD WIDTH=25% ALIGN=CENTER>")


Response.Write(mobjValues.OptionControl(0, "optSInterestE", GetLocalResourceObject("optSInterestE_1Caption"),  , "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("                                    <TD WIDTH=25% ALIGN=LEFT>")


Response.Write(mobjValues.OptionControl(0, "optSInterestE", GetLocalResourceObject("optSInterestE_2Caption"),  , "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    								<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    								<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("                                </TR>" & vbCrLf)
Response.Write("    			 				<TR>" & vbCrLf)
Response.Write("                                    <TD WIDTH=25%><LABEL>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    								<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    								<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    								<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    							</TR> " & vbCrLf)
Response.Write("    			 				<TR>" & vbCrLf)
Response.Write("                                    <TD WIDTH=25% ALIGN=CENTER>")


Response.Write(mobjValues.OptionControl(0, "optSTimeExa", GetLocalResourceObject("optSTimeExa_1Caption"),  , "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	                            <TD WIDTH=25% ALIGN=LEFT>")


Response.Write(mobjValues.OptionControl(0, "optSTimeExa", GetLocalResourceObject("optSTimeExa_2Caption"),  , "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    								<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    								<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    			 				</TR>" & vbCrLf)
Response.Write("                             ")

				Else
Response.Write("" & vbCrLf)
Response.Write("                   			   <TR>" & vbCrLf)
Response.Write("    						       <TD WIDTH=25%><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    							   <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    							   <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    							   <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    						   </TR> " & vbCrLf)
Response.Write("                               <TR>" & vbCrLf)
Response.Write("                                   <TD WIDTH=25% ALIGN=CENTER>")


Response.Write(mobjValues.OptionControl(0, "optSInterestE", GetLocalResourceObject("optSInterestE_1Caption"),  , "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("                                   <TD WIDTH=25% ALIGN=LEFT>")


Response.Write(mobjValues.OptionControl(0, "optSInterestE", GetLocalResourceObject("optSInterestE_2Caption"), CStr(1), "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    							   <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    							   <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("                               </TR>" & vbCrLf)
Response.Write("    						   <TR>" & vbCrLf)
Response.Write("                                   <TD WIDTH=25%><LABEL>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    							   <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    							   <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    							   <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    						   </TR>                               " & vbCrLf)
Response.Write("                               <TR>" & vbCrLf)
Response.Write("                                   <TD WIDTH=25% ALIGN=CENTER>")


Response.Write(mobjValues.OptionControl(0, "optSTimeExa", GetLocalResourceObject("optSTimeExa_1Caption"),  , "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	                           <TD WIDTH=25% ALIGN=LEFT>")


Response.Write(mobjValues.OptionControl(0, "optSTimeExa", GetLocalResourceObject("optSTimeExa_2Caption"), CStr(1), "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    							   <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("    							   <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("                               </TR>" & vbCrLf)
Response.Write("                             ")

				End If
			End If
		End If
	End If
Response.Write("" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <!----------------------------------------------------------FIN Metodo de calculo------------------------------------------>" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("   <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("     <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HIGHLIGHTED""><LABEL><A NAME=""Cobro"">" & GetLocalResourceObject("AnchorCobroCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("     </TR>" & vbCrLf)
Response.Write("     <TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""5"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("     </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=5>&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=2 CLASS=""HIGHLIGHTED""><LABEL>" & GetLocalResourceObject("Anchor12Caption") & "</LABEL></TD>		" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=2 CLASS=""HIGHLIGHTED""><LABEL>" & GetLocalResourceObject("Anchor13Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	<TR> " & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=2 CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("		<TD></TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=2 CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("     </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("         " & vbCrLf)
Response.Write("     <TR>" & vbCrLf)
Response.Write("         <TD><LABEL>" & GetLocalResourceObject("tcnDefaultiCaption") & "</LABEL>&nbsp;")


Response.Write(mobjValues.NumericControl("tcnIntDelay", 4, CStr(mobjOptionInstall.nIntdelay),  , GetLocalResourceObject("tcnIntDelayToolTip"), False, 2))


Response.Write("</TD> " & vbCrLf)
Response.Write("         <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("         <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("         <TD><LABEL>" & GetLocalResourceObject("cboOptCommCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("         <TD>")


Response.Write(mobjValues.PossiblesValues("cboOptComm", "Table251", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nOpt_comm),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboOptCommToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("     </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("     <TR>" & vbCrLf)
Response.Write("         <TD>")


Response.Write(mobjValues.CheckControl("chkSchDelUp", GetLocalResourceObject("chkSchDelUpCaption"), mobjOptionInstall.sCh_del_up,  , "Enabled(this, ""CollectionUp"")"))


Response.Write("</TD>" & vbCrLf)
Response.Write("         <TD><LABEL>" & GetLocalResourceObject("tcnDefaultiCaption") & "</LABEL>" & vbCrLf)
Response.Write("         ")


Response.Write(mobjValues.NumericControl("tcnIntDelUp", 6, CStr(mobjOptionInstall.nInt_del_up),  , GetLocalResourceObject("tcnIntDelUpToolTip"),  , 2,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("         <TD >&nbsp;</TD>" & vbCrLf)
Response.Write("         <TD>")


Response.Write(mobjValues.CheckControl("chkSchOptComm", GetLocalResourceObject("chkSchOptCommCaption"), mobjOptionInstall.sCh_opt_com))


Response.Write("</TD>" & vbCrLf)
Response.Write("         <TD >&nbsp;</TD>" & vbCrLf)
Response.Write("     </TR> " & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("     <TR>" & vbCrLf)
Response.Write("       	<TD>")


Response.Write(mobjValues.CheckControl("chkSchDelDown", GetLocalResourceObject("chkSchDelDownCaption"), mobjOptionInstall.sCh_del_down,  , "Enabled(this, ""CollectionDown"")"))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tcnDefaultiCaption") & "</LABEL>" & vbCrLf)
Response.Write("           ")


Response.Write(mobjValues.NumericControl("tcnIntDelDown", 6, CStr(mobjOptionInstall.nInt_del_down),  , GetLocalResourceObject("tcnIntDelDownToolTip"), False, 2,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD >&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tcnLevelDraCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("   		<TD>")


Response.Write(mobjValues.NumericControl("tcnLevelComm", 5, CStr(mobjOptionInstall.nLevel_comm),  , GetLocalResourceObject("tcnLevelCommToolTip"), False, False))


Response.Write("</TD>" & vbCrLf)
Response.Write("     </TR>" & vbCrLf)
Response.Write(" " & vbCrLf)
Response.Write("     <TR>" & vbCrLf)
Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tcnLevelDraCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;&nbsp;&nbsp;&nbsp;")


Response.Write(mobjValues.NumericControl("tcnLevelDelay", 5, CStr(mobjOptionInstall.nLevel_delay),  , GetLocalResourceObject("tcnLevelDelayToolTip"), False, False))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD> 	" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD> 	 	" & vbCrLf)
Response.Write("     </TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=2 CLASS=""HIGHLIGHTED""><LABEL>" & GetLocalResourceObject("Anchor14Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD> 	" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD> 	" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD> 	" & vbCrLf)
Response.Write("	<TR>  " & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=2 CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("      " & vbCrLf)
Response.Write("     <TR>" & vbCrLf)
Response.Write("	    <TD><LABEL>" & GetLocalResourceObject("tcnDsctoPagCaption") & "<LABEL></TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;&nbsp;&nbsp;&nbsp;")


Response.Write(mobjValues.NumericControl("tcnDsctoPag", 4, CStr(mobjOptionInstall.nDscto_pag),  , GetLocalResourceObject("tcnDsctoPagToolTip"), False, 2))


Response.Write("</TD> " & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("     </TR>" & vbCrLf)
Response.Write("     <TR>" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("tcnDsctoAmoCaption") & "</LABEL></td>" & vbCrLf)
Response.Write("	    <TD>&nbsp;&nbsp;&nbsp;&nbsp;")


Response.Write(mobjValues.NumericControl("tcnDsctoAmo", 18, CStr(mobjOptionInstall.nDscto_amo),  , GetLocalResourceObject("tcnDsctoAmoToolTip"), False, 6))


Response.Write("</TD> " & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("     </TR>       " & vbCrLf)
Response.Write("     <TR>" & vbCrLf)
Response.Write("   	    <TD><LABEL>" & GetLocalResourceObject("cboCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;&nbsp;&nbsp;&nbsp;")


Response.Write(mobjValues.PossiblesValues("cboCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nCurrency),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("     </TR>" & vbCrLf)
Response.Write("     <TR>" & vbCrLf)
Response.Write("       <TD>")


Response.Write(mobjValues.CheckControl("chkSchPayUp", GetLocalResourceObject("chkSchPayUpCaption"), mobjOptionInstall.sCh_pay_up,  , "Enabled(this, ""PayUp"")"))


Response.Write("</TD>" & vbCrLf)
Response.Write("       <TD><LABEL>" & GetLocalResourceObject("tcnDsctoPagCaption") & "<LABEL>" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.NumericControl("tcnPayUp", 6, CStr(mobjOptionInstall.nPay_up),  , GetLocalResourceObject("tcnPayUpToolTip"), False, 2,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR> " & vbCrLf)
Response.Write("     <TR>" & vbCrLf)
Response.Write("      	<TD>")


Response.Write(mobjValues.CheckControl("chkSchPayDown", GetLocalResourceObject("chkSchPayDownCaption"), mobjOptionInstall.sCh_pay_down,  , "Enabled(this, ""PayDown"")"))


Response.Write("</TD>" & vbCrLf)
Response.Write("      	<TD><LABEL>" & GetLocalResourceObject("tcnDsctoPagCaption") & "<LABEL>" & vbCrLf)
Response.Write("           ")


Response.Write(mobjValues.NumericControl("tcnPayDown", 6, CStr(mobjOptionInstall.nPay_down),  , GetLocalResourceObject("tcnPayDownToolTip"), False, 2,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("     </TR>" & vbCrLf)
Response.Write(" " & vbCrLf)
Response.Write("     <TR>	" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnLevelPayCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;&nbsp;&nbsp;&nbsp;")


Response.Write(mobjValues.NumericControl("tcnLevelPay", 5, CStr(mobjOptionInstall.nLevel_pay),  , GetLocalResourceObject("tcnLevelPayToolTip"), False, False))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("     </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	If Not mobjValues.ActionQuery Then
	Response.Write("<SCRIPT>VerifyFields(); </" & "Script>")
    End If
	
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjOptionInstall = New eGeneral.OptFinance
mobjValues = New eFunctions.Values
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MFI023"

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT>   	
<SCRIPT>

//VerifyFields: Verifica que si los campos estan chequeados se desbloquean los porcentajes
//------------------------------------------------------------------------------
function VerifyFields(){
//------------------------------------------------------------------------------
	with (self.document.forms[0]){
        if (chkSchUp.checked)
            tcnIntUp.disabled=false
        if (chkSchDown.checked)
            tcnIntDown.disabled=false;
        if (chkSchDelUp.checked)
            tcnIntDelUp.disabled=false;
        if (chkSchDelDown.checked)        
            tcnIntDelDown.disabled=false;
        if (chkSchPayUp.checked)
            tcnPayUp.disabled=false;
        if (chkSchPayDown.checked)
            tcnPayDown.disabled=false;
	}
}
//Enabled: habilita y desabilita los campos Disminuir y Aumentar
//------------------------------------------------------------------------------
function Enabled(Field, Frame){
//------------------------------------------------------------------------------
    switch(Frame){
	    case 'InterestUp':
	        if(Field.checked)
                self.document.forms[0].tcnIntUp.disabled=false;
	    	else
	    	    self.document.forms[0].tcnIntUp.disabled=true;
            break;	    	
	    case 'InterestDown':
	        if(Field.checked)
                self.document.forms[0].tcnIntDown.disabled=false;
	    	else
	    	    self.document.forms[0].tcnIntDown.disabled=true;
	    	break;
	    case 'CollectionUp':
	        if(Field.checked)
	            self.document.forms[0].tcnIntDelUp.disabled=false;
	    	else
	            self.document.forms[0].tcnIntDelUp.disabled=true;
	    	break;
	    case 'CollectionDown':
	        if(Field.checked)
	            self.document.forms[0].tcnIntDelDown.disabled=false;
	    	else
	            self.document.forms[0].tcnIntDelDown.disabled=true;
	    	break;
	    case 'PayUp':
	        if(Field.checked)
	            self.document.forms[0].tcnPayUp.disabled=false;
	    	else
	            self.document.forms[0].tcnPayUp.disabled=true;
	    	break;
	    case 'PayDown':
	        if(Field.checked)
	            self.document.forms[0].tcnPayDown.disabled=false;
	    	else
	            self.document.forms[0].tcnPayDown.disabled=true;
	}
}
</SCRIPT>   	
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("MFI023"))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	MobjMenu = New eFunctions.Menues
	mobjNetFrameWork.sSessionID = Session.SessionID
	mobjNetFrameWork.nUsercode = Session("nUsercode")
	Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
	Response.Write(MobjMenu.setZone(2, "MFI023", "MFI023.aspx"))
End If
MobjMenu = Nothing

' Este codigo fisicamente corresponde a MFI023A VVN
Response.Write(mobjValues.ShowWindowsName("MFI023"))
%>
<FORM METHOD="POST" ID="FORM" NAME="MFI023" ACTION="valMantGeneral.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <P ALIGN="Center">
		<LABEL><A HREF="#Financiamiento"> <%= GetLocalResourceObject("AnchorFinanciamiento2Caption") %></LABEL></A><LABEL> | </LABEL>
        <LABEL><A HREF="#Cobro"> <%= GetLocalResourceObject("AnchorCobro2Caption") %></A>
	</P>
<%

'+ Se realiza la lectura de los valores cargados en la tabla de la opciones de instalación financiamiento
mobjOptionInstall.Find()
Call insPreMFI023A()

'+ Boton de inicio 
Response.Write(mobjValues.BeginPageButton)

%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 11.58.23
Call mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>





