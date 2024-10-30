<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Songs.aspx.cs" Inherits="WebAppPoliMusicV2.Songs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de Canciones</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2 id="lblWelcome" runat="server"></h2>
        <asp:GridView ID="gvSongs" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="ID_SONG" HeaderText="ID" />
                <asp:BoundField DataField="SONG_NAME" HeaderText="Nombre de la Canción" />
                <asp:TemplateField HeaderText="Reproducir">
                    <ItemTemplate>
                        <asp:Button ID="btnPlay" runat="server" Text="Play" CommandArgument='<%# Eval("SONG_PATH") %>' OnClick="btnPlay_Click" />
                        <asp:Button ID="btnPause" runat="server" Text="Pause" OnClientClick="pauseAudio(); return false;" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PLAYS" HeaderText="Veces Tocada" />
            </Columns>
        </asp:GridView>

        <br />
        <asp:Button ID="btnLogout" runat="server" Text="Logout" PostBackUrl="~/Logout.aspx" />
        <br /><br />

        <audio id="audioPlayer" controls="controls" style="display:none;"></audio>

        <script type="text/javascript">
            var currentAudio = document.getElementById('audioPlayer');

            function playAudio(path) {
                if (currentAudio.src !== path) {
                    currentAudio.src = path;
                }
                currentAudio.play();
            }

            function pauseAudio() {
                currentAudio.pause();
            }
        </script>
    </form>
</body>
</html>
