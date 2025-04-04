Public Class createtable
    Inherits System.Web.UI.Page
    Protected ConnStr As String = ConfigurationManager.ConnectionStrings("ConnStr").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        create_sql_table()
        Response.Redirect("WebForm1.aspx")
    End Sub
    Protected Sub create_sql_table()
        Dim iosql As New iosc.iosql
        If iosql.SQLCheckTableExist(“table1”, ConnStr) = False Then
            iosql.createiotable(ConnStr, “table1”, iosql.Ret_CreateTblStr(“table1”, "ID:id,Data1:s,Num1:i,Num2:i,Total:i"))
        End If
    End Sub

End Class