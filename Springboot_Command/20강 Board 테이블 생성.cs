
# Board Class 생성

@Entity
public class Board {
	
    @Id				// Primary Key
    @GeneratedValue(strategy = GenerationType.IDENTITY)		// auto_increament
    private int id;
    
    @Column(nullable = false, length = 100)
    private String title;
    
    @Lob                            // 대용량 데이터를 담을 때 쓰는 어노테이션
    private String content;		    // 섬머노트 라이브러리 ( 일반 글이 디자인이되어서 들어가는데 html 태그가 섞여서 들어간다 그래서 글자의 용량이 커진다 )
    
    @ColumnDefault("0")			    // 얘는 int니까 ' '가 필요 없다.
    private int count; 				// 조회수
    
    @ManyToOne   					// Many = Board / One = User : 한명의 유저는 여러개의 게시글을 쓸수 있다.
    @JoinColumn(name = "userId")	// 데이터베이스의 컬럼으로는 userId로 만들어질거에요~
    private User user; 				// DB는 오브젝트를 저장할 수 없다. FK, 자바는 오브젝트를 저장할 수 있다.
                                    // 자바에서는 Object를 저장할 수 있다, 데이터베이스에는 오브젝트를 저장할 수 없다.
                                    // 그렇기 때문에 자바가 데이터베이스에 맞춰서 JPA를 사용하면 오브젝트를 저장할 수 있다. ( FK가 된다 )
    
    @CreationTimestamp		        // 데이터가 insert, update일시 현재시간이 들어감
    private Timestamp createDate;	
}

# @JoinColumn : Outer Join을 말해주는것 같음 ( LEFT OUTER JOIN or RIGHT OUTER JOIN)
=> @JoinColumn은 JPA(Java Perisistence API)에서 엔티티 간 관계를 매핑할 때
    사용되는 어노테이션 입니다.

    @JoinColumn은 외래키(Foregin Key)컬럼을 매핑할 때 사용됩니다.
    만약에 어떤 엔티티가 다른 엔티티를 참조하고 있다면, 이를 표현하기 위해 외래키 컬럼을
    사용합니다.
    
    @JoinColumn을 사용하여 매핑할 때는, 매핑할 대상 엔티티에서 매핑할 컬럼을 선택할 수 있습니다.
    이렇게 매핕된 엔티티는 연관 엔티티의 외래키를 가리키게 됩니다.

