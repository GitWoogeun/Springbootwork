# MySQL의 기본 격리 수준

MySQL은 InnoDB의 Storyge 엔진을 사용 합니다.

[ READ COMMIT과 Repeatable ]

"Read Commit"의 격리 수준
하나의 트랜잭션이 데이터베이스에서 변경한 내용이 커밋되기 전까지 다른 트랜잭션에서 해당 
내용을 읽을 수 없습니다. 즉, 커밋되기 전까지는 해당 트랜잭션에서 변경한 내용이 
다른 트랜잭션에서 보이지 않습니다.

"Read Commit" 예시
트랜잭션 A와 B가 있다고 가정합니다. A가 특정 데이터를 업데이트하고 커밋하기 전까지 해당 
데이터를 읽을 수 없습니다. 따라서 B는 해당 데이터를 읽을 때 A가 업데이트한 내용을 볼수 없습니다.
이후 A가 해당 데이터를 커밋하면 B는 해당 내용을 읽을 수 있습니다.


[ Read Commit 소스코드 ]
import java.sql.*;

public class ReadCommitExample {
    public static void main(String[] args) throws SQLException {
        Connection conn = DriverManager.getConnection("jdbc:mysql://localhost/mydatabase", "username", "password");
        conn.setAutoCommit(false);
        // JDBC 격리수준 : 커밋된 내용만 다른 트랜잭션에서 볼 수 있다.
        conn.setTransactionIsolation(Connection.TRANSACTION_READ_COMMITTED);
        
        // Transaction A
        Statement stmtA = conn.createStatement();
        stmtA.executeUpdate("UPDATE mytable SET mycolumn = 'newvalue' WHERE id = 1");

        // Transaction B
        Statement stmtB = conn.createStatement();
        ResultSet rsB = stmtB.executeQuery("SELECT mycolumn FROM mytable WHERE id = 1");

        // B는 A가 업데이트한 'newvalue'를 볼 수 없음
        if (rsB.next()) {
            System.out.println(rsB.getString("mycolumn")); // 기존의 값 출력
        }

        // A 커밋
        conn.commit();

        // B는 이제 A가 업데이트한 'newvalue'를 볼 수 있음
        rsB = stmtB.executeQuery("SELECT mycolumn FROM mytable WHERE id = 1");
        if (rsB.next()) {
            System.out.println(rsB.getString("mycolumn")); // 'newvalue' 출력
        }
        
        conn.close();
    }
}


"Repeatable"의 격리 수준
하나의 트랜잭션이 데이터베이스에서 읽은 내용이 다른 트랜잭션에서 변경되어도 해당 내용을 읽을 때마다
항상 동일한 결과를 반환합니다. 따라서 같은 쿼리를 반복 실행해도 항상 같은 결과를 반환 합니다.

"Repeatable"의 예시
트랜잭션 A가 특정 범위 내의 데이터를 조회하는 쿼리를 실행 합니다.
이후 다른 트랜잭션 B가 해당 범위 내의 데이터를 업데이트하고 커밋합니다.
하지만 "Repeatable" 격리 수준에서는 A가 다시 해당 범위 내의 데이터를 
조회할 때마다 이전과 동일한 결과를 반환 합니다. 따라서 
A는 B의 업데이트를 볼 수 없습니다.

[ Repeatable 소스코드 ]
import java.sql.*;

public class RepeatableExample {
    public static void main(String[] args) throws SQLException {
        Connection conn = DriverManager.getConnection("jdbc:mysql://localhost/mydatabase", "username", "password");
        conn.setAutoCommit(false);
        // Connection.TRANSACTION_REPEATABLE_READ은 JDBC에서 하는 격리 수준 중 하나
        // 커밋된 내용만 다른 트래잭션에서 볼수 있다.
        conn.setTransactionIsolation(Connection.TRANSACTION_REPEATABLE_READ);

        // Transaction A
        Statement stmtA = conn.createStatement();
        ResultSet rsA = stmtA.executeQuery("SELECT mycolumn FROM mytable WHERE id BETWEEN 1 AND 5");

        while (rsA.next()) {
            System.out.println(rsA.getString("mycolumn"));
        }

        // Transaction B
        Statement stmtB = conn.createStatement();
        stmtB.executeUpdate("UPDATE mytable SET mycolumn = 'newvalue' WHERE id = 3");
        conn.commit();

        // A는 B의 업데이트를 볼 수 없음
        rsA = stmtA.executeQuery("SELECT mycolumn FROM mytable WHERE id BETWEEN 1 AND 5");
        while (rsA.next()) {
            System.out.println(rsA.getString("mycolumn")); // 이전의 값들만 출력
        }
        
        conn.close();
    }
}

# 요약 :
"Read Commit"은 커밋되기 전까지 변경 내용을 다른 트랜잭션에서 볼수 없고
"Repeatable"은 같은 쿼리를 반복 실행해도 항상 동일한 결과를 반환 합니다.


"PHANTOM READ"
Phantom Read는 데이터베이스에서 격리 수준을 유지하기 위해 트랜잭션이 실행되는 동안
다른 트랜잭션에 의해 새로운 레코드가 삽입되는 현상을 말합니다.
예를 들어, 트랜잭션 A가 WHERE절에 따라 데이터를 선택한 후,
트랜잭션 B가 새로운 데이터를 추가하면, 트랜잭션 A는 동일한 WHERE절을 사용하여 다시 선택할 때
새로 추가된 데이터를 포함하게 됩니다.
이렇게 되면 트랜잭션 A에서는 처음에 선택했을 때와 다른 결과를 얻게 되는데 이것이 PHANTOM READ 현상 입니다.;


"PHANTOM READ 방지할 수 있는 방법 중 하나 Serializable 격리수준"
PHANTOM READ를 방지하기 위해서는 Serializable 격리 수준을 사용하거나, 다른 격리 수준에서도 적절한 수준을 선택하고,
트랜잭션의 범위와 조건을 적절하게 설정하는 것이 중요합니다.

// PHANTOM READ가 발생할 가능성이 있는 테이블에 대해 
// 잠금을 걸어서 다른 트랜잭션이 데이터를 변경하지 못하도록 막는 예시입니다.

// 데이터베이스 연결 설정
Connection connection = DriverManager.getConnection(DB_URL, DB_USER, DB_PASSWORD);
connection.setAutoCommit(false); // 자동 커밋 비활성화

// 트랜잭션 시작
try {
    // 테이블의 행을 잠금(lock)으로 설정
    PreparedStatement lockStatement = connection.prepareStatement("SELECT * FROM my_table WHERE id = ? FOR UPDATE");
    lockStatement.setInt(1, id);
    ResultSet lockResult = lockStatement.executeQuery();

    // 테이블 조회
    PreparedStatement selectStatement = connection.prepareStatement("SELECT * FROM my_table WHERE id = ?");
    selectStatement.setInt(1, id);
    ResultSet selectResult = selectStatement.executeQuery();

    // 데이터 변경 로직

    // 트랜잭션 커밋
    connection.commit();
} catch (SQLException ex) {
    // 트랜잭션 롤백
    connection.rollback();
} finally {
    // 데이터베이스 연결 종료
    connection.close();
}

[ Serializable의 문제점(중요) ]
Serializable 격리수준은 문제가 있습니다.
Serializable 격리수준에서는 각각의 트랜재개션이 일련 잠금(lock)을 요구하게 되어
데이터베이스 성능이 크게 저하될 수 있습니다.;


"SELECT를 할때 @Transaction을 붙이는 이유"
=> @Transaction 어노테이션은 데이터베이스에서 트랜잭션을 처리할 때 사용됩니다.
   SELECT 쿼리에서 @Transaction 어노테이션을 사용하는 이유는 다음과 같습니다.

   첫째 = @Transaction 어노테이션을 사용하여 트랜잭션 범위를 설정함으로써,
         여러 쿼리에서 발생할수 있는 일관성 문제를 방지할 수 있습니다.
         예를 들어 SELECT 쿼리를 여러 번 실행하는 경우, 데이터의 일관성을 보장하기 위해
         트랜잭션을 설정해야 합니다. 이를 위해 @Transaction 어노테이션을 사용하여 쿼리를
         실행하기 전에 트랜잭션을 시작하고, 쿼리 실행 후에 트랜잭션을 종료할 수 있습니다.

    둘째 = @Transaction 어노테이션을 사용하여 트랜잭션 롤백을 처리할 수 있습니다.
          SELECT 쿼리에서도 예외가 발생할 수 있으며, 이 때 롤백을 처리하여 
          데이터의 일관성을 보장할 수 있습니다.

따라서, SELECT 쿼리에서도 @Transaction 어노테이션을 사용하여 트랜잭션 처리함으로써
데이터 일관성과 무결성을 보장할 수 있습니다.

[ Spring Boot JPA를 사용하여 @Transactional 사용한 SELECT절 예시 ];

@Service
public class ProductService {

    @Autowired
    private ProductRepository productRepository;

    @Transactional(readOnly = true)
    public List<Product> getProductsByName(String name) {
        return productRepository.findByNameContaining(name);
    }
}


"데이터의 정합성"
데이터의 정합성은 데이터가 일관되고 정확하게 유지되는 것을 의미 합니다.
데이터베이스에서 데이터는 여러 사용자 및 애플리케이션에서 동시에 접근되기 때문에
데이터의 일관성과 정확성을 보장하기 위해서는 여러 가지의 요소들을 고려해야 합니다.

" 데이터의 정합성 보장 5가지 "
1. [ 데이터 타입 및 제약 조건 설정 ]
=> 데이터베이스 테이블의 컬럼에 적절한 데이터 타입을 지정하고, 해당 컬럼에 대해 
   필요한 제약조건(NULL, UNIQUE, FOREING KEY 제약 등)을 설정하여 데이터의 정합성을 
   유지할 수 있습니다.;

2. [ 트랜잭션 처리 ] 
=> 트랜잭션 처리를 통해 데이터의 일관성을 유지할 수 있습니다. 트랜잭션은 여러 개의 데이터 
   조작 작업을 묶어서 원자적으로 처리하는 것을 의미 합니다. 즉, 모든 작업이 성공할 때만 커밋되고, 
   작업 중 하나라도 실패하면 롤백 해야 합니다.;

3. [ 격리 수준 설정 ]
=> 격리 수준은 트랜잭션의 동시성 처리 방법을 결정하는 것으로, 격리 수준을 높일수록 데이터 일관성은 보장되지만 처리 성능은 저하 됩니다.;

4. [ 무결성 제약 조건 ]
=> 데이터베이스에서 무결성 제약 조건을 설정하여 데이터의 일관성을 유지할 수 있습니다. 무결성 제약 조건은 데이터베이스에 저장되는
   데이터의 규칙을 정의하는 것으로, 예를 들어 PRIMARY KEY, FORIGN KEY, CHECK 등이 있습니다.;

5. [ 백업과 복구 ]
=> 데이터베이스에서는 정기적으로 백업을 수행하여 데이터의 안정성을 보장할 수 있습니다. 또한, 데이터 손실이나 오류 등이 발생했을 때에는
   백업 데이터를 사용하여 복구 작업을 수행할 수 있습니다.

"데이터의 부정합성"
데이터베이스 내에 저장된 데이터의 상태가 일관성이 없는 상태를 말합니다.
예를 들어 동일한 데이터가 다른 값으로 저장되어 있는 경우,
데이터의 중복이 발생한 경우,
또는 연관된 데이터가 서로 일치하지 않은 경우 등을 말합니다.;
