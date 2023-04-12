
# SpringBoot Repository 메소드 구현 방법

Spring JPA의 userRepository는 데이터베이스에 접근하는 데 사용 됩니다.
이 userRepository를 사용하여 데이터베이스에서 데이터를 가져오고 저장할 수 있습니다.

userRepository에서 메소드를 구현하는 방법은 다음과 같습니다.

1. 메소드 이름 생성

 - 메소드 이름은 기본적으로 "find", "get", "query", "read"등의 단어로 시작해야 합니다.
 
 - 이후에 "By", "First", "Top", "Distinct", "All"등의 단어를 사용하여 메소드의 목적을
   더욱 명확하게 나타냅니다.

- 메소드 이름 뒤에는 검색 조건을 지정하는 키워드를 추가할 수 있습니다.
  이 키워드는 "And", "Or", "Between", "LessThan", "GreaterThan"등이 될 수 있습니다.

2. 메소드 시그니처 작성
- 메소드 시그니처는 메소드의 반환 유형과 파라미터 유형을 정의 합니다.

- 반환 유형은 일반적으로 엔티티 클래스나 Optional<엔티티클래스> 입니다.

- 파라미터는 검색 조건에 해당하는 필드 이름과 값을 받습니다.

3. 메소드 구현
- 메소드에서는 JpaRepository에서 제공하는 메소드를 사용하여 데이터베이스 접근합니다.

- Spring Data JPA는 JpaRepository를 상속하는 인터페이스에 대해 작동으로 구현 클래스를 생성 합니다.

- 따라서 개발자는 메소드의 구현에 집중하여 비즈니스 로직을 작성할 수 있습니다.

예를 들어 userRepository에서 이름으로 사용자를 검색하는 메소드를 구현하려면 다음과 같이
할수 있습니다.
1. 메소드 이름 생성 : findByFirstName
2. 메소드 시그니처 작성: List<User>findByFirstName(String firstName)
3. 메소드 구현: JpaRepository에서 제공하는 findBy 메소드를 사용하여 firstName 필드와 일치하는
   사용자를 찾아 반환 합니다.

이제 userRepository에서 이 메소드를 호출하여 사용자를 검색할 수 있습니다.


[ userRepository를 이용한 예제 소스 ]

"User 클래스 (엔티티)"
@Entity
@Table(name = "users")
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "first_name")
    private String firstName;

    @Column(name = "last_name")
    private String lastName;

    // 생성자, getter, setter 생략
}

"UserService"
@Service
public class UserService {
    @Autowired
    private UserRepository userRepository;

    public List<User> getUsersByFirstName(String firstName) {
        return userRepository.findByFirstName(firstName);
    }

    public List<User> getUsersByLastName(String lastName) {
        return userRepository.findByLastName(lastName);
    }

    public List<User> getUsersByFullName(String firstName, String lastName) {
        return userRepository.findByFirstNameAndLastName(firstName, lastName);
    }

    public List<User> getUsersByEitherName(String firstName, String lastName) {
        return userRepository.findByFirstNameOrLastName(firstName, lastName);
    }

    public List<User> getUsersByPartialName(String firstName) {
        return userRepository.findByFirstNameLike("%" + firstName + "%");
    }

    public List<User> getUsersByPartialLastName(String lastName) {
        return userRepository.findByLastNameContaining(lastName);
    }

    public List<User> getUsersByFirstNameOrderedByLastName(String firstName) {
        return userRepository.findByFirstNameOrderByLastNameAsc(firstName);
    }

    public List<User> getUserByFirstName3ByOrderByLastNameDesc(String fisrName){
        return userRepository.findFirst3ByOrderByLastNameDesc(firstName);
    }
}


"UserRepository"
@Repository
public interface UserRepository extends JpaRepository<User, Long> {
    
    // 필드와 일치하는 User 엔티티의 목록을 반환 합니다.
    List<User> findByFirstName(String firstName);

    // 필드와 일치하는 User 엔티티 목록을 반환 합니다.
    List<User> findByLastName(String lastName);

    // 필드와 lastName 필드가 모두 일치하는 User 엔티티를 반환 합니다.
    List<User> findByFirstNameAndLastName(String firstName, String lastName);
    
    // 필드 또는 lastName 필드 중 하나라도 일치하는 User 엔티티 목록을 반환 합니다.
    List<User> findByFirstNameOrLastName(String firstName, String lastName);

    // 필드에서 지정한 문자열을 포함한 User 엔티티의 목록을 반환 합니다.
    List<User> findByFirstNameLike(String firstName);

    // 필드에서 지정한 문자열을 포함하는 User 엔티티의 목록을 반환합니다.
    List<User> findByLastNameContaining(String lastName);

    // 필드와 일치하는 User 엔티티의 목록을 lastName 필드의 오름차순으로 정렬하여 반환합니다.
    List<User> findByFirstNameOrderByLastNameAsc(String firstName);

    // 필드의 내림차순으로 정렬된 상위 3개 User 엔티티의 목록을 반환합니다.
    List<User> findFirst3ByOrderByLastNameDesc();
}
