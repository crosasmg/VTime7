<%@ Control Language="vb" AutoEventWireup="true" Inherits="AppointmentGITForm" CodeFile="AppointmentGITForm.ascx.vb" %>
<table class="dxscAppointmentForm" cellpadding="0" cellspacing="0" style="width: 100%; height: 230px;" border="0">
	<tr>
		<td class="dxscDoubleCell" colspan="2">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblSubject" runat="server" AssociatedControlID="tbSubject" 
                            Text="Subject:" meta:resourcekey="lblSubjectResource1">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbSubject" runat="server" 
                            Width="100%" 
                            Text='<%# (CType(Container, SchedulerContainer)).Appointment.Subject %>' 
                            meta:resourcekey="tbSubjectResource1"  MaxLength="50">
                            <Paddings PaddingLeft="8px" />
                            <BackgroundImage HorizontalPosition="left" ImageUrl="../../../images/generaluse/required.png" Repeat="NoRepeat" VerticalPosition="center"/>
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
                             <RequiredField IsRequired='True' ErrorText="" />
                            </ValidationSettings>
                            </dxe:ASPxTextBox> 
                            

					</td>
				</tr>
			</table>
		</td>
	</tr>
	<% If Not Session("IsNewToDoTask") And Not IsTask Then%>
	<tr>
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblStartDate" runat="server" 
                            AssociatedControlID="edtStartDate" Text="Start time:" Wrap="false" 
                            meta:resourcekey="lblStartDateResource1">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxDateEdit ClientInstanceName="_dx" ID="edtStartDate" runat="server" 
                            Width="100%" Date='<%# (CType(Container, SchedulerContainer)).Start %>' 
                            EditFormat="DateTime" meta:resourcekey="edtStartDateResource1" />
					</td>
				</tr>
			</table>
		</td>
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell" style="padding-left: 25px;">
						<dxe:ASPxLabel runat="server" ID="lblEndDate" Text="End time:" Wrap="false" 
                            AssociatedControlID="edtEndDate" meta:resourcekey="lblEndDateResource1"/>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxDateEdit id="edtEndDate" runat="server" ClientInstanceName="_dx" Date='<%# (CType(Container, SchedulerContainer)).End %>'
							EditFormat="DateTime" Width="100%" meta:resourcekey="edtEndDateResource1">
						</dxe:ASPxDateEdit>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<% End If %>
	<tr>
	    <td class="dxscSingleCell">	
		    <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblPriority" runat="server" AssociatedControlID="cbPriority" 
                            Text="Priority:" Wrap="false" meta:resourcekey="lblPriorityResource1">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxComboBox ClientInstanceName="_dx" ID="cbPriority" runat="server" 
                            Width="100%" ValueType="System.String" meta:resourcekey="cbPriorityResource1" >
                            <Items>
                                <dxe:ListEditItem Text="Low" Value="1" 
                                    meta:resourcekey="ListEditItemResource1" />
                                <dxe:ListEditItem Text="Standard" Value="2"  Selected="true"
                                    meta:resourcekey="ListEditItemResource2" />
                                <dxe:ListEditItem Text="High" Value="3" 
                                    meta:resourcekey="ListEditItemResource3" />
                            </Items>						
						</dxe:ASPxComboBox>
					</td>
				</tr>
			</table>	
        </td>	
	    <td class="dxscSingleCell">	
		    <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell" style="padding-left: 25px;">
						<dxe:ASPxLabel ID="lblTaskStatus" runat="server" 
                            AssociatedControlID="cbTaskStatus" Text="Task Status:" Wrap="false" 
                            meta:resourcekey="lblTaskStatusResource1">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxComboBox ClientInstanceName="_dx" ID="cbTaskStatus" runat="server" 
                            Width="100%" ValueType="System.String" 
                            meta:resourcekey="cbTaskStatusResource1" >						
                            <Items>
                                <dxe:ListEditItem Text="Not Initiated" Value="1" 
                                    meta:resourcekey="ListEditItemResource4" />
                                <dxe:ListEditItem Text="Pending" Value="2" 
                                    meta:resourcekey="ListEditItemResource5" />
                                <dxe:ListEditItem Text="Completed" Value="3" 
                                    meta:resourcekey="ListEditItemResource6" />
                                <dxe:ListEditItem Text="Waiting" Value="4" 
                                    meta:resourcekey="ListEditItemResource7" />
                                <dxe:ListEditItem Text="Deferred" Value="5" 
                                    meta:resourcekey="ListEditItemResource8" />
                                <dxe:ListEditItem Text="Cancelled" Value="6" 
                                    meta:resourcekey="ListEditItemResource9" />
                            </Items>						
						</dxe:ASPxComboBox>
					</td>
				</tr>
			</table>	
        </td>	    
	</tr>	
	<tr> 
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblLocation" runat="server" AssociatedControlID="tbLocation" 
                            Text="Location:" meta:resourcekey="lblLocationResource1">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbLocation" runat="server" 
                            Width="100%" 
                            Text='<%# (CType(Container, SchedulerContainer)).Appointment.Location %>' 
                            meta:resourcekey="tbLocationResource1" />
					</td>
				</tr>
			</table>
		</td>
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell" style="padding-left: 25px;">
						<dxe:ASPxLabel ID="lblLabel" runat="server" AssociatedControlID="edtLabel" 
                            Text="Label:" meta:resourcekey="lblLabelResource1">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtLabel" runat="server" 
                            Width="100%" 
                            DataSource='<%# (CType(Container, SchedulerContainer)).LabelDataSource %>' 
                            meta:resourcekey="edtLabelResource1" ValueType="System.String" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblStatus" runat="server" AssociatedControlID="edtStatus" 
                            Text="Show time as:" Wrap="false" meta:resourcekey="lblStatusResource1">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtStatus" runat="server" 
                            Width="100%" 
                            DataSource='<%# (CType(Container, SchedulerContainer)).StatusDataSource %>' 
                            meta:resourcekey="edtStatusResource1" ValueType="System.String" />
					</td>
				</tr>
			</table>
		</td>
		<td class="dxscSingleCell" style="padding-left: 22px;">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
			</table>
		</td>
	</tr>
	<tr>
        <td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblResource" runat="server" 
                            AssociatedControlID="edtResource" Text="Resource:" 
                            meta:resourcekey="lblResourceResource1">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
<%--						<dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtResource" runat="server" 
                            Width="100%" 
                            DataSource='<%# (CType(Container, SchedulerContainer)).ResourceDataSource %>' 
                            Enabled='<%# (CType(Container, SchedulerContainer)).CanEditResource %>' 
                            meta:resourcekey="edtResourceResource1" ValueType="System.String" />--%>

<%
   If ResourceSharing Then
%>
						<dxe:ASPxDropDownEdit id="ddResource" runat="server" Width="100%" 
                                              ClientInstanceName="ddResource" 
                                              Enabled='<%#(CType(Container, SchedulerContainer)).CanEditResource%>' 
                                              AllowUserInput="false" meta:resourcekey="edtResourceResource1" 
                                              SkinID="CheckComboBox">
							<DropDownWindowTemplate>
								<dxe:ASPxListBox id="edtMultiResource" runat="server" width="100%" 
                                                 SelectionMode="CheckColumn" 
                                                 DataSource='<%#ResourceDataSource%>' Border-BorderWidth="0" 
                                                 SkinID="CheckComboBoxListBox" >
									<ClientSideEvents SelectedIndexChanged="function(s, e) {
										var resourceNames = new Array();
										var items = s.GetSelectedItems();
										var count = items.length;
										if (count > 0) {
											for(var i=0; i<count; i++) 
												resourceNames.push(items[i].text);
										}
										else
											resourceNames.push(ddResource.cp_Caption_ResourceNone);
										ddResource.SetValue(resourceNames.join(', '));
									}"></ClientSideEvents>
								</dxe:ASPxListBox>
							</DropDownWindowTemplate>
						</dxe:ASPxDropDownEdit>                        
<%
   Else
%>
						<dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtResource" runat="server" Width="100%" DataSource='<%#ResourceDataSource%> ' Enabled='<%#(CType(Container, SchedulerContainer)).CanEditResource%>' meta:resourcekey="edtResourceResource1"  />
<%
   End If
%>

					</td>
				</tr>
			</table>
		</td>
<%
   If CanShowReminders Then
%>
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell" style="padding-left: 22px;">
						<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
							<tr>
								<td style="width: 20px; height: 20px;">
									<dxe:ASPxCheckBox ClientInstanceName="_dx" ID="chkReminder" runat="server" 
                                        meta:resourcekey="chkReminderResource1"> 
										<ClientSideEvents CheckedChanged="function(s, e) { OnChkReminderCheckedChanged(s, e); }" />
									</dxe:ASPxCheckBox>
								</td>
								<td style="padding-left: 2px;">
									<dxe:ASPxLabel ID="lblReminder" runat="server" Text="Reminder" 
                                        AssociatedControlID="chkReminder" meta:resourcekey="lblReminderResource1" />
								</td>
							</tr>
						</table>
					</td>
					<td class="dxscControlCell" style="padding-left: 3px">
						<dxe:ASPxComboBox  ID="cbReminder" 
                            ClientInstanceName="_dxAppointmentForm_cbReminder" runat="server" Width="100%" 
                            DataSource='<%# (CType(Container, SchedulerContainer)).ReminderDataSource %>' 
                            meta:resourcekey="cbReminderResource1" ValueType="System.String" />
					</td>
				</tr>
			</table>
		</td>
<%
   End If
%>
	</tr>
	<tr>
		<td class="dxscSingleCell" valign="top">
			<dxe:ASPxCheckBox ID="IndividualTaskIndicator" runat="server" 
                CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" 
                CssPostfix="Office2003_Olive" 
                Text="The task is to be done by one person only" 
                Checked ='<%# (CType(Container, SchedulerContainer)).IndividualTaskIndicator %>' 
                meta:resourcekey="IndividualTaskIndicatorResource1">
            </dxe:ASPxCheckBox>
		</td>    
	    <td class="dxscSingleCell" style="padding-left: 25px;" vertical-align="top">	
		    <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
                        <dxe:ASPxCheckBox ClientInstanceName="_dx" ID="chkAllDay" 
                            Text="All day activity" runat="server" 
                            Checked='<%# (CType(Container, SchedulerContainer)).Appointment.AllDay %>' 
                            meta:resourcekey="chkAllDayResource1"> 
			            </dxe:ASPxCheckBox>
					</td>					
				</tr>
			</table>	
        </td>			
    </tr>
	<tr>
		<td class="dxscSingleCell" valign="top">
            <dxe:ASPxCheckBox ID="WarningWhenCompleted" runat="server" 
                Text="Send a message when the task is completed" 
                meta:resourcekey="WarningWhenCompletedResource1">
            </dxe:ASPxCheckBox>
		</td>    
	    <td class="dxscSingleCell" style="padding-left: 25px;" vertical-align="top">	
		    <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblPerCompleted" runat="server" 
                            AssociatedControlID="tbPerCompleted" Text="% Completed:" Wrap="false" 
                            meta:resourcekey="lblPerCompletedResource1">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbPerCompleted" runat="server" 
                            Width="30px" MaxLength="3"                            
                            meta:resourcekey="tbPerCompletedResource1" >
                            <MaskSettings Mask="&lt;0..100&gt;" />
                        </dxe:ASPxTextBox>
					</td>					
				</tr>
			</table>	
        </td>			
    </tr>    	
    <tr>
		<td class="dxscSingleCell"  valign="top">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblTransaction" runat="server" 
                            AssociatedControlID="cbTransaction" Text="Transaction:" 
                            meta:resourcekey="lblTransactionResource1">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxComboBox ID="cbTransaction" runat="server" ValueType="System.String" 
                            Width="100%" meta:resourcekey="cbTransactionResource1">
                            <Items>
                                <dxe:ListEditItem Text="Tratamiento de pólizas" Value="CA001_K" 
                                    meta:resourcekey="ListEditItemResource10" />
                                <dxe:ListEditItem Text="Tratamiento de siniestros" Value="SI001" 
                                    meta:resourcekey="ListEditItemResource11" />
                            </Items>
                        </dxe:ASPxComboBox>
					</td>
				</tr>
			</table>		
            
		</td>
		<td class="dxscSingleCell"  valign="top">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0" width="100%" >
				<tr>
					<td class="dxscLabelCell" style="padding-left: 25px;">
						<dxe:ASPxLabel ID="lblVisualTimeTransactionAction" runat="server" 
                            AssociatedControlID="cbVisualTimeTransactionAction" Text="Action:" 
                            meta:resourcekey="lblVisualTimeTransactionActionResource1"  Visible="false">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell" valign="top">
						<dxe:ASPxComboBox ID="cbVisualTimeTransactionAction" runat="server"  
                            Width="100%" meta:resourcekey="cbVisualTimeTransactionActionResource1" 
                            ValueType="System.String" Visible="false">
                        </dxe:ASPxComboBox>
                        
					</td>
				</tr>
			</table>		
            
		</td>
	</tr>	
	<tr>
		<td class="dxscDoubleCell" colspan="2" style="height: 90px;">
			<dxe:ASPxMemo ClientInstanceName="_dx" ID="tbDescription" runat="server" 
                Width="100%" Rows="6" 
                Text='<%# (CType(Container, SchedulerContainer)).Appointment.Description %>' 
                meta:resourcekey="tbDescriptionResource1"  />
		</td>
	</tr>
	<% If Not Session("IsNewToDoTask") And Not IsTask Then%>	
	<tr>
		<td class="dxscSingleCell" valign="top">
<%--            <dxe:ASPxCheckBox ID="AlarmActive" runat="server" Text="Alarm Active" 
                meta:resourcekey="AlarmActiveResource1">
                <ClientSideEvents CheckedChanged="function(s, e) { OnAlarmActiveCheckedChanged(s, e); }" />
            </dxe:ASPxCheckBox>--%>
		</td> 
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell" style="padding-left: 25px;">
<%--						<dxe:ASPxLabel runat="server" ID="lblAlarmDateTime" Text="Alarm Date" 
                            Wrap="false" AssociatedControlID="edtAlarmDateTime" 
                            meta:resourcekey="lblAlarmDateTimeResource1"/>--%>
					</td>
					<td class="dxscControlCell">
<%--						<dxe:ASPxDateEdit id="edtAlarmDateTime" runat="server" 
                            ClientInstanceName="_dxAppointmentForm_edtAlarmDateTime" Date='<%# (CType(Container, SchedulerContainer)).End %>'
							EditFormat="DateTime" Width="100%" meta:resourcekey="edtAlarmDateTimeResource1">
						</dxe:ASPxDateEdit>--%>

					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>    
		<td class="dxscSingleCell" valign="top">
            <dxe:ASPxCheckBox ID="RepeatActive" runat="server" Text="Repeat Active" 
                meta:resourcekey="RepeatActiveResource1">
                <ClientSideEvents CheckedChanged="function(s, e) { OnRepeatActiveCheckedChanged(s, e); }" />
            </dxe:ASPxCheckBox>
		</td> 
	</tr>
	<tr>
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblRepeatStatingDate" runat="server" 
                            AssociatedControlID="edtRepeatStatingDate" Text="Starting time:" Wrap="false" 
                            meta:resourcekey="lblRepeatStatingDateResource1">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxDateEdit ClientInstanceName="_dxAppointmentForm_edtRepeatStatingDate" 
                            ID="edtRepeatStatingDate" runat="server" Width="100%" 
                            Date='<%# (CType(Container, SchedulerContainer)).RepeatStartingDate %>' 
                            EditFormat="DateTime" meta:resourcekey="edtRepeatStatingDateResource1" />
					</td>
				</tr>
			</table>
		</td>
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell" style="padding-left: 25px;">
						<dxe:ASPxLabel runat="server" ID="lblRepeatEndingDate" Text="Ending time:" 
                            Wrap="false" AssociatedControlID="edtRepeatEndingDate" 
                            meta:resourcekey="lblRepeatEndingDateResource1"/>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxDateEdit id="edtRepeatEndingDate" runat="server" 
                            ClientInstanceName="_dxAppointmentForm_edtRepeatEndingDate" 
                            Date='<%# (CType(Container, SchedulerContainer)).RepeatEndingDate %>'
							EditFormat="DateTime" Width="100%" meta:resourcekey="edtRepeatEndingDateResource1">
						</dxe:ASPxDateEdit>
					</td>
				</tr>
			</table>
		</td>
	</tr>	
	<tr>
		<td class="dxscSingleCell">
			<table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell">
						<dxe:ASPxLabel ID="lblRepeatFrecuency" runat="server" 
                            AssociatedControlID="tbRepeatFrecuency" Text="Frecuency" Wrap="false" 
                            meta:resourcekey="lblRepeatFrecuencyResource1">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxComboBox ID="tbRepeatFrecuency" runat="server" Width="100%" 
                            ClientInstanceName="_dxAppointmentForm_tbRepeatFrecuency" 
                            ValueType="System.String"                             
                            meta:resourcekey="tbRepeatFrecuencyResource1" >
                            <Items>
                                <dxe:ListEditItem Text="Daily" Value="1" 
                                    meta:resourcekey="ListEditItemResource12" />
                                <dxe:ListEditItem Text="Weekly" Value="2" 
                                    meta:resourcekey="ListEditItemResource13" />
                                <dxe:ListEditItem Text="Monthly" Value="3" 
                                    meta:resourcekey="ListEditItemResource14" />
                                <dxe:ListEditItem Text="Annually" Value="4" 
                                    meta:resourcekey="ListEditItemResource15" />
                            </Items>
                        </dxe:ASPxComboBox>
					</td>
				</tr>
			</table>
		</td>
		<td class="dxscSingleCell">
		    <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
				<tr>
					<td class="dxscLabelCell" style="padding-left: 25px;">
						<dxe:ASPxLabel ID="lblRepeatTimes" runat="server" 
                            AssociatedControlID="tbRepeatTimes" Text="Times" Wrap="false" 
                            meta:resourcekey="lblRepeatTimesResource1">
						</dxe:ASPxLabel>
					</td>
					<td class="dxscControlCell">
						<dxe:ASPxTextBox ClientInstanceName="_dxAppointmentForm_tbRepeatTimes" 
                            ID="tbRepeatTimes" runat="server" 
                            Width="30px" MaxLength="3" 
                            Text='<%# (CType(Container, SchedulerContainer)).RepeatTimes %>' 
                            meta:resourcekey="tbRepeatTimesResource1" >
                            <MaskSettings Mask="&lt;0..100&gt;" />
                        </dxe:ASPxTextBox>
					</td>					
				</tr>
			</table>
		</td>
	</tr>
	<% End If %>
</table>

<dxe:ASPxComboBox ID="cbRecordType" runat="server"  
Width="100%"  visible="False"
ValueType="System.String">
    <Items>
        <dxe:ListEditItem Text="Task" Value="1"  />
        <dxe:ListEditItem Text="Event" Value="2"  />
    </Items>
</dxe:ASPxComboBox>
                        

<dxsc:AppointmentRecurrenceForm ID="AppointmentRecurrenceForm1" runat="server" 
	DayNumber='<%# (CType(Container, SchedulerContainer)).RecurrenceDayNumber %>' 
	End='<%# (CType(Container, SchedulerContainer)).RecurrenceEnd %>' 
	Month='<%# (CType(Container, SchedulerContainer)).RecurrenceMonth %>' 
	OccurrenceCount='<%# (CType(Container, SchedulerContainer)).RecurrenceOccurrenceCount %>' 
	Periodicity='<%# (CType(Container, SchedulerContainer)).RecurrencePeriodicity %>' 
	RecurrenceRange='<%# (CType(Container, SchedulerContainer)).RecurrenceRange %>' 
	Start='<%# (CType(Container, SchedulerContainer)).RecurrenceStart %>' 
	WeekDays='<%# (CType(Container, SchedulerContainer)).RecurrenceWeekDays %>' 
	WeekOfMonth='<%# (CType(Container, SchedulerContainer)).RecurrenceWeekOfMonth %>' 
	RecurrenceType='<%# (CType(Container, SchedulerContainer)).RecurrenceType %>'
    
    IsFormRecreated='<%# (CType(Container, SchedulerContainer)).IsFormRecreated %>' 
    EnableScriptSupport="False" 
    meta:resourcekey="AppointmentRecurrenceForm1Resource1" >
</dxsc:AppointmentRecurrenceForm>

<table cellpadding="0" cellspacing="0" style="width: 100%; height: 35px;">
	<tr>
		<td style="width: 100%; height: 100%;" align="center">
			<table style="height: 100%;">
				<tr>
					<td>
						<dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnOk" Text="OK" 
                            UseSubmitBehavior="False" AutoPostBack="False" 
							EnableViewState="False" Width="140px" meta:resourcekey="btnOkResource1"/>
					</td>
					<td>
						<dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnCancel" 
                            Text="Cancel" UseSubmitBehavior="False" AutoPostBack="False" EnableViewState="False" 
							Width="140px" CausesValidation="False" meta:resourcekey="btnCancelResource1" />
					</td>
					<td>
						<dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnDelete" 
                            Text="Delete" UseSubmitBehavior="False"
							AutoPostBack="False" EnableViewState="False" Width="140px"
							Enabled='<%# (CType(Container, SchedulerContainer)).CanDeleteAppointment %>'
							CausesValidation="False" meta:resourcekey="btnDeleteResource1" />
					</td>
					<td>
						<dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnCallVT" 
                            Text="Call Transaction" UseSubmitBehavior="False"
							AutoPostBack="False" EnableViewState="False" Width="140px"
							CausesValidation="False" meta:resourcekey="btnCallVTResource1" />
					</td>
					<td class="dxscControlCell" align=right valign="top">
					    <dxe:ASPxImage ID="imgWorkFlow" 
					                   runat="server" 					                   
                                       ImageUrl="~/images/32x32/General/workflowMedium.jpg" 
                            meta:resourcekey="imgWorkFlowResource1">
                        </dxe:ASPxImage>
                    </td>
					
				</tr>
			</table>
		</td>
	</tr>
</table>
<table cellpadding="0" cellspacing="0" style="width: 100%;">
	<tr>
		<td style="width: 100%;" align="left">
			<dxsc:ASPxSchedulerStatusInfo runat="server" ID="schedulerStatusInfo" 
                Priority="1" 
                MasterControlId='<%# (CType(Container, SchedulerContainer)).ControlId %>' 
                meta:resourcekey="schedulerStatusInfoResource1" />
        </td>
	</tr>
</table>
<script id="dxss_ASPxSchedulerAppoinmentForm" type="text/javascript">
	function OnChkReminderCheckedChanged(s, e) {
		var isReminderEnabled = s.GetValue();
		if (isReminderEnabled)
			_dxAppointmentForm_cbReminder.SetSelectedIndex(3);
		else
			_dxAppointmentForm_cbReminder.SetSelectedIndex(-1);

		_dxAppointmentForm_cbReminder.SetEnabled(isReminderEnabled);

}

function OnAlarmActiveCheckedChanged(s, e) {
    var isAlarmActived = s.GetValue();
    _dxAppointmentForm_edtAlarmDateTime.SetEnabled(isAlarmActived);
}

function OnRepeatActiveCheckedChanged(s, e) {
    var isAlarmActived = s.GetValue();
    _dxAppointmentForm_edtRepeatStatingDate.SetEnabled(isAlarmActived);
    _dxAppointmentForm_edtRepeatEndingDate.SetEnabled(isAlarmActived);
    _dxAppointmentForm_tbRepeatFrecuency.SetEnabled(isAlarmActived);
    _dxAppointmentForm_tbRepeatTimes.SetEnabled(isAlarmActived);
}
      
</script>
