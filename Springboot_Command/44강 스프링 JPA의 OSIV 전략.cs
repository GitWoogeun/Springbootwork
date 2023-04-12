
# 스프링 JPA의 OSIV 전략
[ 스프링부트의 전통적인 트랜잭션방식의 문제점 해결 ]

개선 방법
┌───────────────┐    ┌─────────────┐    ┌───────────────┐    ┌─────────────┐  ┌───────┐
│ 사용자 Request│  => │ Controller │  => │   Service    │  => │ repository │ =>│  DB  │   
│   ( 요청 )    │  <= │   ( 분기 ) │  <= │ (   송금  )   │ <= │ (CRUD 실행) │ <=│      │ 
└───────────────┘    └─────────────┘    └───────────────┘    └─────────────┘  └───────┘

Spring JPA의 OSIV 전략
=> Spring JPA에서 OSIV(Object-In-View)전략은 영속성 컨텍스트를 View 계층 (View layer)까지
   유지하는 전략입니다.;

   이 전략은 HTTP 요청이 들어오면 영속성 컨텍스트를 생성하고, HTTP 응답이 완료되면
   영속성 컨텍스트를 종료합니다. 이 때 모든 Entity 객체들은 영속성 컨텍스트에 의해 관리 됩니다.
   따라서 View 계층에서도 필요한 경우 Entity 객체를 수정하거나 조회 할 수 있습니다.

   이 전략의 장점은 코드의 간결함과 개발자가 영속성 컨텍스트의 관리할 필요가 없다는 것
   입니다. 또한 영속성 컨텍스트를 유지하므로 지연 로딩(Lazy loading)이나
   즉시 로딩(Eager loading)과 같은 성능 최적화 기법을 사용할 수 있습니다.

   하지만, 이 전략은 Entity 객체의 수가 많거나 View 계층에서 Entity 객체를 수정할 필요가
   없는 경우에는 성능 문제를 야기할 수 있습니다. 또한 영속성 컨텍스트의 크기가 계속해서 
   증가하면서 Out Of Memory(OOM)예외가 발생할 수 있습니다.

   따라서 OSIV 전략은 적절한 상황에서 사용해야 하며 Entity 객체의 수나 View 계층에서의
   Entity 객체 사용 여부를 고려하여 사용해야 합니다.

[ JPA의 OSIV 전략 소스 코드 ];

@Service
@Transactional
public class UserService {
    
    @Autowired
    private UserRepository userRepository;
    
    public User getUserById(Long id) {
        return userRepository.findById(id).orElse(null);
    }
    
    public User saveUser(User user) {
        return userRepository.save(user);
    }
    
    public void deleteUser(Long id) {
        userRepository.deleteById(id);
    }
    
    @Transactional(readOnly = true)
    public List<User> getAllUsers() {
        return userRepository.findAll();
    }
}

위의 예시에서는 'UserService' 클래스에서 Spring JPA의 'UserRepository'를 사용하여
데이터베이스의 'User' 엔티티를 조회, 추가, 수정, 삭제하는 기능을 구현하고 있습니다.

@Transactional 어노테이션은 해당 메소드가 하나의 트랜잭션으로 실행됨을
나타내며 기본적으로; "readOnly" 속성이 "false"로 설정됩니다.
이 경우 메소드 실행중에 변경된 내용이 영속성 컨텍스트에 반영 됩니다.

"getAllUsers()" 메소드에서는 @Transactional(readOnly = true) 어노테이션을 사용하여
읽기 전용 트랜잭션으로 설정 하였습니다. 이 경우, 해당 메소드가 실행될 떄 영속성 컨텍스트에 
Entity 객체들이 로딩되고, View 계층에서 Entity 객체를 조회할 수 있습니다.

위의 예시에서는 OSIV 전략을 명시적으로 사용하지 않았지만
@Transactional 어노테이션을 사용하여 트랜잭션 범위 내에서 영속성 컨텍스트를 
사용하고 있습니다. 이를 통해 OSIV 전략이 자동으로 적용됩니다.


[ 스프링 JPA의 OSIV 전략의 다른 예시 ];
@Configuration
@EnableJpaRepositories(basePackages = "com.example.repository")
@EnableTransactionManagement
public class PersistenceConfig {
    
    @Bean
    public EntityManagerFactory entityManagerFactory() {
        HibernateJpaVendorAdapter vendorAdapter = new HibernateJpaVendorAdapter();
        vendorAdapter.setGenerateDdl(true);

        LocalContainerEntityManagerFactoryBean factory = new LocalContainerEntityManagerFactoryBean();
        factory.setJpaVendorAdapter(vendorAdapter);
        factory.setPackagesToScan("com.example.entity");
        factory.setDataSource(dataSource());

        return factory.getObject();ㄷ
    }
    
    @Bean
    public DataSource dataSource() {

        // 데이터베이스 설정
        DriverManagerDataSource dataSource = new DriverManagerDataSource();
        dataSource.setDriverClassName("org.h2.Driver");
        dataSource.setUrl("jdbc:h2:mem:test");
        dataSource.setUsername("sa");
        dataSource.setPassword("");

        return dataSource;
    }
    
    @Bean
    public PlatformTransactionManager transactionManager() {
        JpaTransactionManager txManager = new JpaTransactionManager();
        txManager.setEntityManagerFactory(entityManagerFactory());
        txManager.setDataSource(dataSource());

        return txManager;
    }
    
    @Bean
    public OpenEntityManagerInViewFilter openEntityManagerInViewFilter() {
        return new OpenEntityManagerInViewFilter();
    }
}

위의 예시에서는 entityManagerFactory() 메소드에서 LocalContainerEntityManagerFactoryBean을 사용하여 
JPA 설정을 구성하고 있습니다. 이 설정에서는 HibernateJpaVendorAdapter를 사용하여 Hibernate를 사용하고 있으며, 
dataSource() 메소드에서는 H2 데이터베이스를 사용하도록 구성되어 있습니다. 
마지막으로 transactionManager() 메소드에서는 EntityManagerFactory를 사용하여 
PlatformTransactionManager를 생성하고 있습니다.
