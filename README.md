# dotnetcore-tcpip-ex1
## .net coreのTCPIP練習

Exercise for TCPIP communication by .net core.<br>
Just the simple exersice for localhost communication.
  - Program "client" send request to "server" and "server" reponses.
  - Print the process content to console screen.
  - Request and respone contents are use UTF8 for asia text.
  - Requests are like:
  
        { id:10, content:"あいうえお" }
        { id:20, content:"かきくけこ" }
        { id:00, content:"stop the server" } // Client send this request to stop the server program.
  - Reponses are like:
  
        { id:11, content:"'あいうえお' processed" }
        { id:21, content:"'かきくけこ' processed" }
        { id:00, content:"server starts to stop" } // Response and stop the server