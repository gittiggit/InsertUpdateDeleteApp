<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SPOutputParameters.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td>Enter your name : </td><td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Select gender : </td><td>
                <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="128px">
                    <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                    
                </asp:DropDownList></td>
        </tr>
        <tr>
             <td>Salary : </td><td>
                 <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
             <td>Select department : </td><td>
                 <asp:DropDownList ID="DropDownList2" runat="server" Height="16px" Width="126px">
                     <asp:ListItem Text="CSTE" Value="1"></asp:ListItem>
                     <asp:ListItem Text="ACCE" Value="2"></asp:ListItem>
                 </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" Width="106px" /></td>
        </tr>
        <tr style="column-span:2">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Your Id is : "></asp:Label></td>
        </tr>

    </table>

    </div>
    </form>
</body>
</html>
