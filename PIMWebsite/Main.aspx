<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="PIMWebsite.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Parallel Image Manipulator</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<asp:Label ID="lblinputFile" runat="server" Text="Select a jpg, png, or bmp file to manipulate: "></asp:Label>
			<asp:FileUpload ID="FileUpload1" runat="server" />
			<br />

			<div>
				<asp:Button ID="btnInvert" runat="server" Text="Invert" OnClick="btnInvert_Click" Visible="false" />
			</div>

			<br />

			<div>
				<asp:Label ID="Label4" runat="server" Text="How would you like to filter the image?"></asp:Label>
				<asp:DropDownList ID="ddlFilter" runat="server">
					<asp:ListItem Value="R">R</asp:ListItem>
					<asp:ListItem Value="G">G</asp:ListItem>
					<asp:ListItem Value="G">G</asp:ListItem>
				</asp:DropDownList>
				<asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" />
			</div>
			<br />

			<div>
				<%--Rotate--%>
				<asp:Label ID="Label1" runat="server" Text="How many times would you like to rotate the image?"></asp:Label>
				<asp:TextBox ID="txtRotateTimes" runat="server" TextMode="Number"></asp:TextBox>
				<br />
				<asp:Label ID="Label2" runat="server" Text="Which direction would you like to rotate the image?"></asp:Label>
				<asp:RadioButtonList ID="radbtnRotateDirection" runat="server">
					<asp:ListItem Value="false">Clockwise</asp:ListItem>
					<asp:ListItem Value="true">Counter-Clockwise</asp:ListItem>
				</asp:RadioButtonList>
				<asp:Button ID="btnRotate" runat="server" Text="Rotate" OnClick="btnRotate_Click" />
			</div>

			<br />
			<br />

			<div>
				<%--Flip--%>
				<asp:Label ID="Label3" runat="server" Text="Which orientation would you like to flip the image?"></asp:Label>
				<asp:RadioButtonList ID="radbtnFlip" runat="server">
					<asp:ListItem Value="true">Vertical</asp:ListItem>
					<asp:ListItem Value="false">Horizontal</asp:ListItem>
				</asp:RadioButtonList>
				<asp:Button ID="btnFlip" runat="server" Text="Flip" OnClick="btnFlip_Click" />
			</div>


		</div>
	</form>
</body>
</html>
