#┌─────────────────────────────────────────────────────────────────────────────────
#│ 카카오 로그인 OAuth2.0 개념 이해
#└─────────────────────────────────────────────────────────────────────────────────

OAuth = Open Auth = 인증 처리를 대신 해준다.


홍길동                          Blog(홈페이지 서버)                      카카오API서버     자원서버
                =>                                         =>
            로그인 요청                               카카오로그인요청

                <=                     
            로그인페이지            

                =>                                         =>
        카카오로그인 버튼 클릭                         카카오로그인 요청
                                       <=
                                    CODE를 보내줌    
                <=                                          <=
인증완료     카카오 동의 창 보여줌                     카카오로그인 화면                       

                                       <=
                                 ACCESS TOKEN 준다

OAuth = 카카오에 접근할 수 있는 주인


[ 전체적인 틀 ]
홍길동은 우리 블로그 서버에게 내 카카오 정보에 접근할 수 있는 권한을 위임 했다고 보면 된다.
=> OAuth 로그인


리소스 오너    : 이 홍길동 정보의 접근할 수 있는 주인
클라이언트     : 블로그 서버 ( 카카오 API서버의 입장에서 보면 하나의 클라이언트 )
카카오API 서버 : OAuth2.0서버( Open Auth == 인증 서버 )
리소스 서버    : 자원 서버 
리소스 오너는 리소스서버에 접근할 수 있는 주인


스프링 공식 OAuth 주체 = > ( FaceBook과 Google )
OAuth_Client 라이브러리 ( FaceBook, Google 인증과 권한처리를 정말 쉽게 처리 해준다. )

