
# 회원가입 시 Ajax를 사용하는 2가지 이유:

첫번째 : 
    요청에 대한 응답을 html이 아닌 Data[JSON]를 받기 위해서

    # 웹 구현일 시
    # 웹 : 이미 화면 디자인을 자바코드로 다 되어있고 회원가입 요청시 HTML파일 리턴
    클라이언트 =========> 회원가입 요청 =========> 서버 ==========> DB
    클라이언트 <======== ( HTML 응답 ) <========= 서버 <========== DB
                                           (HTML 리턴 서버)
    

    클라이언트가 서버에 요청하는게 꼭 브라우저만 있느게 아니라서 
    JSON 데이터로 받을 수 있어야 한다.
    # 앱 구현일 시
    앱[화면 디자인을 안드로이드라면]을 자바코드로 구현
    클라이언트 =========> 회원가입 요청 =========> 서버 ==========> DB
    클라이언트 <========  ( JSON 리턴) <========= 서버 <========== DB
                                        ( 데이터 리턴 서버 )


# JSON으로 회원가입을 하는 이유!
이렇게 된다면 서버를 2개를 구현해야한다. ( HTML 1개 , 데이터 1개 )
이런 번거러움이 없을려면 ajax를 활용하여 JSON으로 데이터 변형 후
데이터를 Ajax로 받고 화면은 서버에게 요청을 하여 응답을 받으면 된다.
그렇게 된다면 서버를 1대만 사용해도 충분하다.

