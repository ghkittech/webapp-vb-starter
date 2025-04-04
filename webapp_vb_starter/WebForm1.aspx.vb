Public Class WebForm1
    Inherits System.Web.UI.Page
    Protected ConnStr As String = ConfigurationManager.ConnectionStrings("ConnStr").ConnectionString
    Protected iosql As New iosc.iosql
    Protected iocc As New iosc.iocc
    Protected QueryString_EditID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        QueryString_EditID = Request.QueryString("Edit")
        If Not IsPostBack Then
            Display_Load_Data()
            If QueryString_EditID = "" Then
                Button1.Text = "Add"
            Else
                Button1.Text = "Edit"
                Display_EditRecord(QueryString_EditID)
                Panel1.Visible = True
                Panel2.Visible = False
            End If
        End If
    End Sub
    Protected Function sumup(ByVal input1 As String, ByVal input2 As String) As Integer
        Return CInt(input1) + CInt(input2)
    End Function
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim StrArr(3) As String
        StrArr(0) = TextBox1.Text
        StrArr(1) = TextBox2.Text
        StrArr(2) = TextBox3.Text
        StrArr(3) = sumup(TextBox2.Text, TextBox3.Text)
        Select Case Button1.Text
            Case "Add"
                Dim ReturnInt As Integer = iosql.AddUpdSQLDataRetInt(ConnStr, "i", "table1", "Data1,Num1,Num2,Total", StrArr)
                QueryString_EditID = ReturnInt
            Case "Edit"
                iosql.AddUpdSQLDataRetInt(ConnStr, "u", "table1", "Data1,Num1,Num2,Total", StrArr, "id", QueryString_EditID)
        End Select
        Response.Redirect("WebForm1.aspx")
    End Sub
    Public Sub Display_Load_Data()
        GridView1.DataSource = iocc.GetDataTable("select * from table1", ConnStr)
        GridView1.DataBind()
    End Sub
    Public Sub Display_EditRecord(ByVal EditID As String)
        Dim StrArr() As String = iocc.DataReturnStrArrayInDb("select Data1,Num1,Num2 from table1 where ID=" & EditID, ConnStr)
        If StrArr(0) <> "Nil" Then
            TextBox1.Text = StrArr(0)
            TextBox2.Text = StrArr(1)
            TextBox3.Text = StrArr(2)
        End If
    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        Select Case RadioButtonList1.SelectedValue
            Case "Input"
                Panel1.Visible = True
                Panel2.Visible = False
            Case "List"
                Panel1.Visible = False
                Panel2.Visible = True
        End Select
    End Sub

End Class