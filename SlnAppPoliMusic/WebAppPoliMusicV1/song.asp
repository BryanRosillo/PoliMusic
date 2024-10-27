<%@ Language="VBScript" %>
<%

If Session("username") = "" Then
    Response.Redirect "login.asp"
End If


Dim conn, rs, sql, username
username = Session("username")

Set conn = Server.CreateObject("ADODB.Connection")
conn.Open "Provider=SQLOLEDB;Data Source=localhost,1433;Initial Catalog=BDD_PoliMusic_GR2;User ID=sa;Password=.Lahobr2001;"


sql = "SELECT SONG_NAME, SONG_PATH, PLAYS FROM TBL_SONG"
Set rs = conn.Execute(sql)
%>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Lista de Canciones</title>
    <style>
        
        body { font-family: Arial, sans-serif; padding: 20px; background-color: #f5f5f5; }
        h1 { color: #333; }
        table { width: 100%; border-collapse: collapse; margin-top: 20px; }
        th, td { padding: 10px; border: 1px solid #ddd; text-align: center; }
        th { background-color: #4c83ff; color: #fff; }
        tr:nth-child(even) { background-color: #f2f2f2; }
        button { padding: 5px 10px; background-color: #4c83ff; color: #fff; border: none; cursor: pointer; border-radius: 5px; }
        button:hover { background-color: #3a6dde; }
        .logout-btn { margin-top: 20px; padding: 10px 20px; background-color: #d9534f; color: #fff; border: none; cursor: pointer; border-radius: 5px; }
        .logout-btn:hover { background-color: #c9302c; }
    </style>
    <script>
        
        let currentAudio = null;

        
        function manageAudioPlayback(audioElement) {
            
            if (currentAudio && currentAudio !== audioElement) {
                currentAudio.pause();
                currentAudio.currentTime = 0; 
            }
            
            currentAudio = audioElement;
        }

        
        document.addEventListener("DOMContentLoaded", function() {
            // Selecciona todos los elementos de audio
            const audioElements = document.querySelectorAll("audio");
            audioElements.forEach(audio => {
                audio.addEventListener("play", function() {
                    manageAudioPlayback(this);
                });
            });
        });
    </script>
</head>
<body>

<h1>Bienvenido, <%=username%></h1>

<table>
    <tr>
        <th>Name</th>
        <th>Song</th>
        <th>Plays</th>
    </tr>
    
    <% 
    ' Listar las canciones en la tabla
    Do While Not rs.EOF 
    %>
    <tr>
        <td><%= rs("SONG_NAME") %></td>
        <td>
            <audio controls>
                <source src="<%= rs("SONG_PATH") %>" type="audio/mpeg">
                Tu navegador no soporta el elemento de audio.
            </audio>
        </td>
        <td><%= rs("PLAYS") %></td>
    </tr>
    <%
        rs.MoveNext
    Loop
    rs.Close
    conn.Close
    Set conn = Nothing
    %>
</table>

<form action="logout.asp" method="post">
    <input type="submit" value="Logout" class="logout-btn">
</form>

</body>
</html>