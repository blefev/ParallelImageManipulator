<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="PIMWebsite.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Parallel Image Manipulator</title>
</head>
<body style="align-content: center;">
    <form id="form1" runat="server">
        <asp:Label ID="lblTitle" runat="server" Visible="true" Text="Parallel Image Manipulator" Font-Size="Large"></asp:Label>
        <br />
        <br />

        <div>
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        </div>
        <asp:Label ID="lblinputFile" runat="server" Text="To begin, select a jpg, png, or bmp file to manipulate: "></asp:Label>
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <div>
            <br />
            <asp:Label ID="Label9" runat="server" Text="Once you have selected a file, you may choose one of the following options to modify your image. "></asp:Label>
            <br />
            <asp:Label ID="Label10" runat="server" Text="After your image has been modified, you can locate it in the source directory. "></asp:Label>

        </div>
       
        <div>________________________________________________________________________________________</div>

        <div>
            <asp:Button ID="btnInvert" runat="server" Text="Invert" OnClick="btnInvert_Click" Visible="false" />
        </div>

        <br />

        <div>
            <asp:Label ID="Label4" runat="server" Text="How would you like to filter the image?"></asp:Label>
            <asp:DropDownList ID="ddlFilter" runat="server">
                <asp:ListItem Value="R">R</asp:ListItem>
                <asp:ListItem Value="G">G</asp:ListItem>
                <asp:ListItem Value="B">B</asp:ListItem>
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

        <div>
            <%--Flip--%>
            <asp:Label ID="Label3" runat="server" Text="Which orientation would you like to flip the image?"></asp:Label>
            <asp:RadioButtonList ID="radbtnFlip" runat="server">
                <asp:ListItem Value="true">Vertical</asp:ListItem>
                <asp:ListItem Value="false">Horizontal</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button ID="btnFlip" runat="server" Text="Flip" OnClick="btnFlip_Click" />

        </div>
        <br />

        <div>
            <%--Negate--%>
            <asp:Label ID="Label5" runat="server" Text="Click Negate if you would like to negate the image"></asp:Label>
            <br />
            <asp:Button ID="btnNegate" runat="server" Text="Negate" OnClick="btnNegate_Click" />
        </div>
        <br />

        <div>
            <%--Grayscale--%>
            <asp:Label ID="Label6" runat="server" Text="Click Grayscale if you would like to grayscale the image"></asp:Label>
            <br />
            <asp:Button ID="btnGrayscale" runat="server" Text="Grayscale" OnClick="btnGrayscale_Click" />
        </div>
        <br />

        <div>
            <%--Blur--%>
            <asp:Label ID="Label7" runat="server" Text="Click Blur if you would like to blur the image"></asp:Label>
            <br />
            <asp:Button ID="btnBlur" runat="server" Text="Blur" OnClick="btnBlur_Click" />
        </div>
        <br />

        <div>
            <%--Brightness--%>
            <asp:Label ID="Label8" runat="server" Text="How much would you like to increase the images brightness?"></asp:Label>
            <asp:TextBox ID="txtBrightness" runat="server" TextMode="Number"></asp:TextBox>
            <br />
            <asp:Button ID="btnBrightness" runat="server" Text="Brightness" OnClick="btnBrightness_Click" />
            <br />
        </div>
    </form>
</body>
</html>
