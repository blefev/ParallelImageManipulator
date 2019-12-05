<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="PIMWebsite.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Parallel Image Manipulator</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<input id="File1" type="file" />

			<asp:Button ID="btnInvert" runat="server" Text="Invert" OnClick="btnInvert_Click" Enabled="false" />

			<asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" />


			<%--Rotate--%>
			<asp:Label ID="Label1" runat="server" Text="How many times would you like to rotate the image?"></asp:Label>
			<asp:TextBox ID="txtRotateTimes" runat="server" TextMode="Number"></asp:TextBox>
			<asp:Label ID="Label2" runat="server" Text="Which direction would you like to rotate the image?"></asp:Label>
			<asp:RadioButtonList ID="radbtnRotateDirection" runat="server">
				<asp:ListItem Value="false">Clockwise</asp:ListItem>
				<asp:ListItem Value="true">Counter-Clockwise</asp:ListItem>
			</asp:RadioButtonList>
			<asp:Button ID="btnRotate" runat="server" Text="Rotate" OnClick="btnRotate_Click" />


			<%--Flip--%>
			<asp:Label ID="Label3" runat="server" Text="Which orientation would you like to flip the image?"></asp:Label>
			<asp:RadioButtonList ID="radbtnFlip" runat="server">
				<asp:ListItem Value="true">Vertical</asp:ListItem>
				<asp:ListItem Value="false">Horizontal</asp:ListItem>
			</asp:RadioButtonList>
			<asp:Button ID="btnFlip" runat="server" Text="Flip" OnClick="btnFlip_Click" />
		</div>
	</form>
</body>
</html>
