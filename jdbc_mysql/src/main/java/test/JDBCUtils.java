package test;



import java.sql.*;

    public class JDBCUtils {
        private static Connection con;

        static {
            try {
                //初始化MySQL的Driver类
                Class.forName("com.mysql.cj.jdbc.Driver");
                String url ="jdbc:mysql://192.168.125.200:3306/testdb?useSSL=false";
                String user = "xl";
                String password ="1010";
                //连接数据库
                con = DriverManager.getConnection(url, user, password);
            } catch(Exception e) {
                throw new RuntimeException(e + ",数据库连接失败！");
            }
        }

        public static Connection getConnection() {
            return con;
        }

        public static void close(Connection con, Statement state) {
            if(con != null) {
                try {
                    con.close();
                } catch (SQLException e) {
                    e.printStackTrace();
                }
            }
            if(state != null) {
                try {
                    state.close();
                } catch (SQLException e) {
                    e.printStackTrace();
                }
            }
        }

        public static void close(Connection con, Statement state, ResultSet rs) {
            if(con != null) {
                try {
                    con.close();
                } catch (SQLException e) {
                    e.printStackTrace();
                }
            }
            if(state != null) {
                try {
                    state.close();
                } catch (SQLException e) {
                    e.printStackTrace();
                }
            }
            if(rs != null) {
                try {
                    rs.close();
                } catch (SQLException e) {
                    e.printStackTrace();
                }
            }
        }
    }