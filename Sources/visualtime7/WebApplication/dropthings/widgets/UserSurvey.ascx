<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserSurvey.ascx.vb" Inherits="Dropthings.Widgets.UserSurveyUserControl" %>
<asp:MultiView ID="MultiViewQuestionnaire" runat="server" ActiveViewIndex="0">
    <asp:View ID="ViewQuestion" runat="server">
        <asp:Table ID="Table2" runat="server" Width="345px">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="QuestionLabel" runat="server" Text="" Font-Italic="True" Font-Size="12px"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Size="12px">
                    </asp:RadioButtonList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="MessageLabel" runat="server" Font-Underline="True" ForeColor="Red" Font-Size="12px">
                    </asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="SubmitButton" runat="server" Text="Submit" BorderStyle="NotSet" />
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" Width="150px">
                    <asp:LinkButton ID="ResultsButton" runat="server" Font-Size="12px" Font-Bold="True">Show Results</asp:LinkButton>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:View>
    <asp:View ID="ViewAnswers" runat="server">
        <asp:Table ID="Table1" runat="server" Width="340px" ForeColor="Gray">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="PollResulLabel" runat="server" Text="Poll Results" Font-Size="12px"
                        Font-Bold="True"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">

                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">

                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">

                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Question1Label" runat="server" Text="" Font-Size="12px" Font-Italic="True"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="NumberResponsesLabel" runat="server" Text="Total Number of Responses:"
                        Font-Size="Small" Font-Italic="True"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="TotalLabel" runat="server" Text="" Font-Bold="True" Font-Size="12px"></asp:Label>
                    <hr />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Option1Label" runat="server" Font-Size="12px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="110px">
                    <asp:Label ID="Response1Label" runat="server" Font-Size="12px"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Option2Label" runat="server" Font-Size="12px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Response2Label" runat="server" Font-Size="12px"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Option3Label" runat="server" Font-Size="12px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Response3Label" runat="server" Font-Size="12px"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Option4Label" runat="server" Font-Size="12px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Response4Label" runat="server" Font-Size="12px"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Option5Label" runat="server" Font-Size="12px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Response5Label" runat="server" Font-Size="12px"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                    <asp:LinkButton ID="QuestionButton" runat="server" Font-Size="12px" Font-Bold="True">Show Question</asp:LinkButton>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:View>
</asp:MultiView>
