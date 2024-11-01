<%@ Language="VBScript" %>
<%

Dim conn, rs, sql, username, password

Set conn = Server.CreateObject("ADODB.Connection")
conn.Open "Provider=SQLOLEDB;Data Source=localhost,1433;Initial Catalog=BDD_PoliMusic_GR2;User ID=sa;Password=PASSWORD;"


username = Request.Form("username")
password = Request.Form("password")

If Request.ServerVariables("REQUEST_METHOD") = "POST" Then
    sql = "SELECT * FROM TBL_USER WHERE username = '" & username & "' AND password = '" & password & "'"
    Set rs = conn.Execute(sql)
    
    If Not rs.EOF Then
        
        Session("username") = rs("username")
        Response.Redirect "song.asp"
    Else
        Response.Write "<p style='color:#d9534f; text-align:center;'>Credenciales incorrectas.</p>"
    End If
    
    rs.Close
End If

conn.Close
Set conn = Nothing
%>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Login</title>
    <style>
        
        body { 
            font-family: Arial, sans-serif; 
            display: flex; 
            align-items: center; 
            justify-content: center; 
            text-align: center;
            min-height: 100vh; 
            background: linear-gradient(135deg, #4c83ff, #91a3ff); 
            margin: 0;
        }

        
        .login-container { 
            width: 100%; 
            max-width: 350px; 
            padding: 2rem; 
            background-color: #ffffff; 
            border-radius: 8px; 
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2); 
            text-align: center;
        }

        
        .login-container h2 { 
            margin: 0 0 1rem; 
            color: #333333; 
            font-size: 1.5rem; 
            font-weight: 600; 
        }

        
        .login-container input[type="text"], 
        .login-container input[type="password"] { 
            width: 100%; 
            padding: 10px; 
            margin: 10px 0; 
            border: 1px solid #ccc; 
            border-radius: 5px; 
            box-sizing: border-box;
            font-size: 1rem;
        }
        
        .login-container input[type="submit"] { 
            width: 100%; 
            padding: 10px; 
            background-color: #4c83ff; 
            color: #ffffff; 
            border: none; 
            border-radius: 5px; 
            cursor: pointer; 
            font-size: 1rem; 
            font-weight: bold;
            transition: background-color 0.3s ease;
        }

        .login-container input[type="submit"]:hover { 
            background-color: #3a6dde; 
        }

        
        .login-container p {
            font-size: 0.9rem;
            color: #d9534f;
            margin-top: 1rem;
        }
    </style>
</head>
<body>

<div class="login-container">
    <h2>Login</h2>
    <form method="post" action="login.asp">
        <input type="text" name="username" placeholder="User" required>
        <input type="password" name="password" placeholder="Password" required>
        <input type="submit" value="Ingresar">
    </form>
</div>

</body>
</html>
