
# JpaRepository를 이용하여 Paging처리 소스
@RestController
public class DummyControllerTest {
		
    # DummyController가 메모리에 뜰떄 UserRepository 또한 같이 메모리에 뜬다. ( Spring이 Componunt할 떄 )
    @Autowired  // 의존성 주입 ( DI )
    private UserRepository userRepository;

    
    # 유저 테이블의 전체 데이터를 List에 담아서 보여준다.
    // http://localhost:8000/blog/bummy/users
    @GetMapping("/dummy/users")
    public List<User> list() {
        # Json View 설치 : Chrome 확장프로그램에서 JsonView 설치
        # User테이블의 전체 리턴
        return userRepository.findAll();
    }    


    # 한 페이지당 2건의 데이터를 리턴 받을것 ( Paging을 해보겠다 )
    # JPA에서 제공하는 Page 오브젝트
    # Paging 기본 전략 : 데이터는 2건씩, sort는 id로, 정렬은 id의 최신식으로 페이징처리 하겠다.
    // http://localhost:8000/blog/dummy/user?page=0  => [ 첫번째 페이지 호출 ( 제일 최신) ]
    // http://localhost:8000/blog/dummy/user?page=1  => [ 두번째 페이지 호출 ]
    @GetMapping("/dummy/user")
    public List<User> PagingList(@PageableDefault(size=2, sort = "id", direction = Sort.Direction.DESC) Pageable pageable) {
        // findAll ( Pageable )	
        // Page<T> findAll(Pageable pageable);
        Page<User> pagingUsers = userRepository.findAll(pageable);

        // Page를 리턴을 했을 때 필요한 데이터가 있기 때문에 List에 형변환해서 리턴을 함    
        List<User> users = pagingUsers.getContent();
        return users;
    }
}