#Region "using"

Imports System.ComponentModel
Imports GIT.EDW.Query.Model
Imports DevExpress.Web.ASPxClasses
Imports DevExpress.Web.ASPxEditors
Imports InMotionGIT.Common.Proxy
Imports System.Threading.Thread
Imports System.Globalization

#End Region

Partial Class Controls_ClaimControl
    Inherits UserControl
    Implements Interfaces.IQueryUserControl

#Region "Private fields"

    Private _repositoryName As String = String.Empty

#End Region

#Region "Public Events"

    Public Event GetClaimTextChanged()

#End Region

#Region "Public properties for userControl"

    ''' <summary>
    ''' Propiedad publica para colocar el nombre del siniestro en el user control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Bindable(True), Browsable(True), Category("More Options"), DefaultValue(""), Description("Set the text of the control.")> _
    Public Property Text() As String
        Get
            Return ClaimComboBox.Value
        End Get
        Set(value As String)
            ClaimComboBox.Value = value
        End Set
    End Property

    Public Property Enabled() As Boolean
        Get
            Return ClaimComboBox.ClientEnabled
        End Get
        Set(value As Boolean)
            ClaimComboBox.ClientEnabled = value
        End Set
    End Property

    Public Property IsAllowSearch() As Boolean
        Get
            Return ClaimComboBox.ClientEnabled
        End Get

        Set(value As Boolean)
            ClaimComboBox.ClientEnabled = value
        End Set
    End Property

    Public Property NullText() As String
        Get
            Return ClaimComboBox.NullText
        End Get
        Set(value As String)
            ClaimComboBox.NullText = value
        End Set
    End Property

#End Region

#Region "Public properties for validation userControl"

    Public Property PaddingLeft() As Unit
        Get
            Return Nothing
        End Get

        Set(value As Unit)
        End Set
    End Property

    Public Property HorizontalPositionImage() As String
        Get
            Return String.Empty
        End Get

        Set(value As String)
        End Set
    End Property

    Public Property ImageUrl() As String
        Get
            Return String.Empty
        End Get
        Set(value As String)
        End Set
    End Property

    Public Property RepeatImage() As BackgroundImageRepeat
        Get
            Return BackgroundImageRepeat.NoRepeat
        End Get
        Set(value As BackgroundImageRepeat)
        End Set
    End Property

    Public Property VerticalPositionImage() As String
        Get
            Return String.Empty
        End Get
        Set(value As String)
        End Set
    End Property

    Public Property ErrorDisplayMode() As ErrorDisplayMode
        Get
            Return ErrorDisplayMode.None
        End Get
        Set(value As ErrorDisplayMode)
        End Set
    End Property

    Public Property IsRequired() As Boolean
        Get
            Return False
        End Get
        Set(value As Boolean)
        End Set
    End Property

    Public Property ErrorText() As String
        Get
            Return String.Empty
        End Get
        Set(value As String)
        End Set
    End Property

#End Region

#Region "Control Events"

    Protected Sub ClaimComboBox_ItemsRequestedByFilterCondition(source As Object, e As ListEditItemsRequestedByFilterConditionEventArgs) Handles ClaimComboBox.ItemsRequestedByFilterCondition
        Dim shortDatePattern As String = CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern

        Dim sql As String = String.Format(CultureInfo.InvariantCulture,
                                          "SELECT NCLAIM, NBRANCH, SBRANCH, NPRODUCT, SPRODUCT, NPOLICY, NCERTIF, SCLIENT, SCLIENAME, DDECLADAT, DOCCURDAT, " &
                                          "SSTACLAIM, SSTACLAIMDESC, NOFFICE, SOFFICE, NOFFICEAGEN, SOFFICEAGEN, NAGENCY, SAGENCY, NLOC_RESERV, NLOC_PAY_AM " &
                                          "FROM (SELECT NCLAIM, NBRANCH, SBRANCH, NPRODUCT, SPRODUCT, NPOLICY, NCERTIF, SCLIENT, SCLIENAME, " &
                                                       "TO_CHAR(DDECLADAT, '{0}') AS DDECLADAT, TO_CHAR(DOCCURDAT, '{0}') AS DOCCURDAT, " &
                                                       "SSTACLAIM, SSTACLAIMDESC, NOFFICE, SOFFICE, NOFFICEAGEN, SOFFICEAGEN, NAGENCY, SAGENCY, NLOC_RESERV, " &
                                                       "NLOC_PAY_AM, ROW_NUMBER() OVER (ORDER BY NCLAIM) ROW_NUM " &
                                          "FROM INSUDB.GCV_CLAIMCONTROL WHERE %FILTER%) Result " &
                                          "WHERE ROW_NUM BETWEEN {1} AND {2}", shortDatePattern, e.BeginIndex + 1, e.EndIndex + 1)

        If String.IsNullOrEmpty(e.Filter) Then
            sql = sql.Replace("%FILTER%", "NCLAIM IS NOT NULL")

        Else
            Dim filter As String = e.Filter.Trim
            Dim isNumber As Boolean = IsNumeric(filter.Replace("%", String.Empty))

            If filter.IndexOf("%") = -1 Then
                filter = String.Format(CultureInfo.InvariantCulture, "%{0}%", filter)
            End If

            If isNumber Then
                sql = sql.Replace("%FILTER%", String.Format("(NCLAIM LIKE '{0}' OR NBRANCH LIKE '{0}')", filter))
            Else
                sql = sql.Replace("%FILTER%", String.Format("(SBRANCH LIKE '{0}' OR SPRODUCT LIKE '{0}')", filter))
            End If
        End If

        Dim policyCbo As ASPxComboBox = DirectCast(source, ASPxComboBox)

        With New DataManagerFactory(sql, "CLAIM", "BackOfficeConnectionString")
            policyCbo.DataSource = .QueryExecuteToTable(True)
            policyCbo.DataBind()
        End With
    End Sub

    Protected Sub ClaimComboBox_ItemRequestedue(source As Object, e As ListEditItemRequestedByValueEventArgs) Handles ClaimComboBox.ItemRequestedByValue
        If Not String.IsNullOrEmpty(e.Value) Then
            With DirectCast(source, ASPxComboBox)
                Dim value As String = e.Value
                If value.IsNotEmpty Then
                    Dim result As System.Data.DataTable
                    Dim shortDatePattern As String = CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern
                    With New DataManagerFactory(String.Format(CultureInfo.InvariantCulture,
                                                              "SELECT NCLAIM, NBRANCH, SBRANCH, NPRODUCT, SPRODUCT, NPOLICY, NCERTIF, SCLIENT, SCLIENAME, " &
                                                                      "TO_CHAR(DDECLADAT, '{0}') AS DDECLADAT, TO_CHAR(DOCCURDAT, '{0}') AS DOCCURDAT, " &
                                                                      "SSTACLAIM, SSTACLAIMDESC, NOFFICE, SOFFICE, NOFFICEAGEN, SOFFICEAGEN, NAGENCY, SAGENCY, NLOC_RESERV, " &
                                                                      "NLOC_PAY_AM FROM INSUDB.GCV_CLAIMCONTROL WHERE NCLAIM = {1}", shortDatePattern, value),
                                                                  "CLAIM", "BackOfficeConnectionString")
                        result = .QueryExecuteToTable(True)
                    End With

                    .DataSource = result
                    .DataBind()
                End If
            End With
        End If
    End Sub

#End Region

#Region "IQueryUserControl Implement"

    Public Property ControlID As String Implements Interfaces.IQueryUserControl.ControlID
        Get
            Return Me.ID
        End Get
        Set(value As String)
            Me.ID = value
            ClaimComboBox.ClientInstanceName = value
        End Set
    End Property

    Public Property Repository As String Implements Interfaces.IQueryUserControl.Repository
        Get
            Return _repositoryName
        End Get
        Set(value As String)
            _repositoryName = value
        End Set
    End Property

    Public Property ToolTip As String Implements Interfaces.IQueryUserControl.ToolTip
        Get
            Return ClaimComboBox.ToolTip
        End Get
        Set(value As String)
            ClaimComboBox.ToolTip = value
        End Set
    End Property

    Public Property Value As Object Implements Interfaces.IQueryUserControl.Value
        Get
            Return ClaimComboBox.Value
        End Get
        Set(value As Object)
            If Not IsNothing(value) AndAlso Not String.IsNullOrEmpty(value) Then
                ClaimComboBox.Value = value

                Dim x = value

                Dim item As ListEditItem = ClaimComboBox.Items.FindByValue(value)

                If item.IsNotEmpty Then
                    item.Selected = True
                End If
            End If
        End Set
    End Property

    Public Property Enabled1 As Boolean Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.Enabled
        Get
            Return ClaimComboBox.ClientEnabled
        End Get

        Set(value As Boolean)
            ClaimComboBox.ClientEnabled = value
        End Set
    End Property

    Public Property Script As String Implements GIT.EDW.Query.Model.Interfaces.IQueryUserControl.Script
        Get
            Return ClaimComboBox.ClientSideEvents.SelectedIndexChanged
        End Get
        Set(value As String)
            ClaimComboBox.ClientSideEvents.SelectedIndexChanged = value
        End Set
    End Property

#End Region

End Class
