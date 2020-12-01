package com.xl.JavaCommonSample;

import com.xl.Helper.LocalDateTimeHelper;
import com.xl.Model.Person;

import java.time.LocalDateTime;
import java.util.*;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class StreamSample {

    public static void main(String args[]) {

        normalTest1();

    }

    public static void parallelTest() {
        long lasttime = LocalDateTimeHelper.LocalDateTimeToMiSecond(LocalDateTime.now());

        ArrayList<String> testList = new ArrayList<>(10000 * 1000);
        for (int i = 0; i < 10000 * 1000; i++) {
            testList.add(String.valueOf(i));
        }

        long curtime = LocalDateTimeHelper.LocalDateTimeToMiSecond(LocalDateTime.now());
        System.out.println("init: " + (curtime - lasttime));
        lasttime = curtime;


        long testCount = testList.stream().filter(string -> ProcessString(string)).count();
        curtime = LocalDateTimeHelper.LocalDateTimeToMiSecond(LocalDateTime.now());
        System.out.println(testCount + " nomal: " + (curtime - lasttime));
        lasttime = curtime;

        testCount = testList.parallelStream().filter(string -> ProcessString(string)).count();
        curtime = LocalDateTimeHelper.LocalDateTimeToMiSecond(LocalDateTime.now());
        System.out.println(testCount + "  parallel: " + (curtime - lasttime));
    }

    public static boolean ProcessString(String input) {
        try {
            Thread.sleep(0);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return input.contains("6");
    }

    public static void normalTest() {
        // 计算空字符串
        List<String> strings = Arrays.asList("abc", "", "bc", "efg", "abcd", "", "jkl");
        System.out.println("列表: " + strings);

        long count = 0;
        List<String> filtered = null;
        String mergedString = "";
        List<Integer> squaresList = null;

        List<Integer> numbers = Arrays.asList(3, 2, 2, 3, 7, 3, 5);
        // 获取列表元素平方数
        List<Integer> integers = Arrays.asList(1, 2, 13, 4, 15, 6, 17, 8, 19);

        // 输出10个随机数
        Random random = new Random();

        System.out.println("使用 Java 8: ");
        System.out.println("列表: " + strings);

        count = strings.stream().filter(string -> string.isEmpty()).count();
        System.out.println("空字符串数量为: " + count);

        count = strings.stream().filter(string -> string.length() == 3).count();
        System.out.println("字符串长度为 3 的数量为: " + count);

        filtered = strings.stream().filter(string -> !string.isEmpty()).collect(Collectors.toList());
        System.out.println("筛选后的列表: " + filtered);

        mergedString = strings.stream().filter(string -> !string.isEmpty()).collect(Collectors.joining(", "));
        System.out.println("合并字符串: " + mergedString);

        squaresList = numbers.stream().map(i -> i * i).distinct().collect(Collectors.toList());
        System.out.println("Squares List: " + squaresList);
        System.out.println("列表: " + integers);

        IntSummaryStatistics stats = integers.stream().mapToInt((x) -> x).summaryStatistics();

        System.out.println("列表中最大的数 : " + stats.getMax());
        System.out.println("列表中最小的数 : " + stats.getMin());
        System.out.println("所有数之和 : " + stats.getSum());
        System.out.println("平均数 : " + stats.getAverage());
        System.out.println("随机数: ");

        random.ints().limit(10).sorted().forEach(System.out::println);

        // 并行处理
        count = strings.parallelStream().filter(string -> string.isEmpty()).count();
        System.out.println("空字符串的数量为: " + count);


    }

    public static void normalTest1() {
        Stream<Integer> stream = Stream.of(1, 2, 3, 4, 5, 6);

        Stream<Integer> stream2 = Stream.iterate(1, (x) -> x * 3).limit(4);
        stream2.forEach(System.out::println);

        Stream<Double> stream3 = Stream.generate(Math::random).limit(3);
        stream3.forEach(System.out::println);
        System.out.println("11111");

        Stream.generate(Math::random).limit(6).filter(x -> x > 0.5).forEach(System.out::println);
        System.out.println("22222");

        List<Integer> list = Arrays.asList(7, 6, 9, 3, 8, 2, 1);
        // 遍历输出符合条件的元素
        list.stream().filter(x -> x > 6).forEach(System.out::println);
        // 匹配第一个
        Optional<Integer> findFirst = list.stream().filter(x -> x > 6).findFirst();
        // 匹配任意（适用于并行流）
        Optional<Integer> findAny = list.parallelStream().filter(x -> x > 6).findAny();
        // 是否包含符合特定条件的元素
        boolean anyMatch = list.stream().anyMatch(x -> x < 6);
        System.out.println("匹配第一个值：" + findFirst.get());
        System.out.println("匹配任意一个值：" + findAny.get());
        System.out.println("是否存在大于6的值：" + anyMatch);

        List<String> filterList = Person.GetTestList().stream().filter(x -> x.getSalary() > 8000).map(Person::getName)
                .collect(Collectors.toList());
        System.out.print("高于8000的员工姓名：" + filterList);


        List<String> list1 = Arrays.asList("adnm", "admmt", "pot", "xbangd", "weoujgsd");

        Optional<String> max = list1.stream().max(Comparator.comparing(String::length));
        System.out.println("最长的字符串：" + max.get());

        Optional<String> max0 = list1.stream().max(new Comparator<String>() {
            @Override
            public int compare(String o1, String o2) {
                if (o1.equals("adnm")) {
                    return 1;
                }
                return o1.compareTo(o2);
            }
        });
        System.out.println("最长的字符串0：" + max0.get());


        List<Integer> list2 = Arrays.asList(7, 6, 9, 4, 11, 6);

        // 自然排序
        Optional<Integer> max1 = list2.stream().max(Integer::compareTo);
        // 自定义排序
        Optional<Integer> max2 = list2.stream().max(new Comparator<Integer>() {
            @Override
            public int compare(Integer o1, Integer o2) {
                if (o1 == 7) {
                    return 1;
                }
                return o1.compareTo(o2);
            }
        });
        System.out.println("自然排序的最大值：" + max1.get());
        System.out.println("自定义排序的最大值：" + max2.get());


        Optional<Person> max3 = Person.GetTestList().stream().max(Comparator.comparing(Person::getSalary));
        System.out.println("员工工资最大值：" + max3.get().getSalary());

        List<Integer> list3 = Arrays.asList(7, 6, 4, 8, 2, 11, 9);
        long count = list3.stream().filter(x -> x > 6).count();
        System.out.println("list中大于6的元素个数：" + count);


        //map
        String[] strArr = {"abcd", "bcdd", "defde", "fTr"};
        List<String> strList = Arrays.stream(strArr).map(String::toUpperCase).collect(Collectors.toList());

        List<Integer> intList = Arrays.asList(1, 3, 5, 7, 9, 11);
        List<Integer> intListNew = intList.stream().map(x -> x + 3).collect(Collectors.toList());

        System.out.println("每个元素大写：" + strList);
        System.out.println("每个元素+3：" + intListNew);


        List<Person> oldList= Person.GetTestList();
        List<Person> personListNew = oldList.stream().map(person -> {
            Person personNew = new Person(person.getName(), 0, 0, null, null);
            personNew.setSalary(person.getSalary() + 10000);
            return personNew;
        }).collect(Collectors.toList());
        System.out.println("一次改动前：" + oldList.get(0).getName() + "-->" + oldList.get(0).getSalary());
        System.out.println("一次改动后：" + personListNew.get(0).getName() + "-->" + personListNew.get(0).getSalary());

        // 改变原来员工集合的方式
        List<Person> oldList2= Person.GetTestList();
        List<Person> personListNew2 = oldList2.stream().map(person -> {
            person.setSalary(person.getSalary() + 10000);
            return person;
        }).collect(Collectors.toList());
        System.out.println("二次改动前：" + oldList2.get(0).getName() + "-->" + oldList2.get(0).getSalary());
        System.out.println("二次改动后：" + personListNew2.get(0).getName() + "-->" + personListNew2.get(0).getSalary());


        List<String> list4 = Arrays.asList("m,k,l,a", "1,3,5,7");
        List<String> listNew = list4.stream().flatMap(s -> {
            // 将每个元素转换成一个stream
            String[] split = s.split(",");
            Stream<String> s2 = Arrays.stream(split);
            return s2;
        }).collect(Collectors.toList());

        System.out.println("处理前的集合：" + list4);
        System.out.println("处理后的集合：" + listNew);

        reduce1();
        reduce();

        collect();
        groupjoin();

    }

    private static void reduce1() {
        List<Integer> list5 = Arrays.asList(1, 3, 2, 8, 11, 4);
        // 求和方式1
        Optional<Integer> sum = list5.stream().reduce((x, y) -> x + y);
        // 求和方式2
        Optional<Integer> sum2 = list5.stream().reduce(Integer::sum);
        // 求和方式3
        Integer sum3 = list5.stream().reduce(0, Integer::sum);

        // 求乘积
        Optional<Integer> product = list5.stream().reduce((x, y) -> x * y);

        // 求最大值方式1
        Optional<Integer> max5 = list5.stream().reduce((x, y) -> x > y ? x : y);
        // 求最大值写法2
        Integer max6 = list5.stream().reduce(1, Integer::max);

        System.out.println("list求和：" + sum.get() + "," + sum2.get() + "," + sum3);
        System.out.println("list求积：" + product.get());
        System.out.println("list求和：" + max5.get() + "," + max6);
    }

    private static void reduce() {


        //重点调试以下几个方法，输出 控制台
        Optional<Integer> sumSalary = Person.GetTestList().stream().map(Person::getSalary).reduce(Integer::sum);
        // 求工资之和方式2：
        Integer sumSalary2 = Person.GetTestList().stream().reduce(0, (sum, p) -> sum += p.getSalary(),
                (sum1, sum2) -> sum1 + sum2);
        // 求工资之和方式3：
        Integer sumSalary3 = Person.GetTestList().stream().reduce(0, (sum, p) -> sum += p.getSalary(), Integer::sum);

        // 求最高工资方式1：
        Integer maxSalary = Person.GetTestList().stream().reduce(0, (max, p) -> max > p.getSalary() ? max : p.getSalary(),
                Integer::max);
        // 求最高工资方式2：
        Integer maxSalary2 = Person.GetTestList().stream().reduce(0, (max, p) -> max > p.getSalary() ? max : p.getSalary(),
                (max1, max2) -> max1 > max2 ? max1 : max2);

        System.out.println("工资之和：" + sumSalary.get() + "," + sumSalary2 + "," + sumSalary3);
        System.out.println("最高工资：" + maxSalary + "," + maxSalary2);
    }

    private static void collect() {
        List<Integer> list = Arrays.asList(1, 6, 3, 4, 6, 7, 9, 6, 20);
        List<Integer> listNew = list.stream().filter(x -> x % 2 == 0).collect(Collectors.toList());
        Set<Integer> set = list.stream().filter(x -> x % 2 == 0).collect(Collectors.toSet());

        Map<?, Person> map = Person.GetTestList().stream().filter(p -> p.getSalary() > 8000)
                .collect(Collectors.toMap(Person::getName, p -> p));
        System.out.println("toList:" + listNew);
        System.out.println("toSet:" + set);
        System.out.println("toMap:" + map);


        Long count =  Person.GetTestList().stream().collect(Collectors.counting());
        // 求平均工资
        Double average = Person.GetTestList().stream().collect(Collectors.averagingDouble(Person::getSalary));
        // 求最高工资
        Optional<Integer> max = Person.GetTestList().stream().map(Person::getSalary).collect(Collectors.maxBy(Integer::compare));
        // 求工资之和
        Integer sum = Person.GetTestList().stream().collect(Collectors.summingInt(Person::getSalary));
        // 一次性统计所有信息
        DoubleSummaryStatistics collect = Person.GetTestList().stream().collect(Collectors.summarizingDouble(Person::getSalary));

        System.out.println("员工总数：" + count);
        System.out.println("员工平均工资：" + average);
        System.out.println("员工工资总和：" + sum);
        System.out.println("员工工资所有统计：" + collect);

    }

    private static void groupjoin() {
        Map<Boolean, List<Person>> part = Person.GetTestList().stream().collect(Collectors.partitioningBy(x -> x.getSalary() > 8000));
        // 将员工按性别分组
        Map<String, List<Person>> group = Person.GetTestList().stream().collect(Collectors.groupingBy(Person::getSex));
        // 将员工先按性别分组，再按地区分组
        Map<String, Map<String, List<Person>>> group2 = Person.GetTestList().stream().collect(Collectors.groupingBy(Person::getSex, Collectors.groupingBy(Person::getArea)));
        System.out.println("员工按薪资是否大于8000分组情况：" + part);
        System.out.println("员工按性别分组情况：" + group);
        System.out.println("员工按性别、地区：" + group2);


        String names = Person.GetTestList().stream().map(p -> p.getName()).collect(Collectors.joining(","));
        System.out.println("所有员工的姓名：" + names);
        List<String> list = Arrays.asList("A", "B", "C");
        String string = list.stream().collect(Collectors.joining("-"));
        System.out.println("拼接后的字符串：" + string);


        // 每个员工减去起征点后的薪资之和（这个例子并不严谨，但一时没想到好的例子）
        Integer sum = Person.GetTestList().stream().collect(Collectors.reducing(0, Person::getSalary, (i, j) -> (i + j - 5000)));
        System.out.println("员工扣税薪资总和：" + sum);

        // stream的reduce
        Optional<Integer> sum2 = Person.GetTestList().stream().map(Person::getSalary).reduce(Integer::sum);
        System.out.println("员工薪资总和：" + sum2.get());


        // 按工资增序排序
        List<String> newList = Person.GetTestList().stream().sorted(Comparator.comparing(Person::getSalary)).map(Person::getName)
                .collect(Collectors.toList());
        // 按工资倒序排序
        List<String> newList2 = Person.GetTestList().stream().sorted(Comparator.comparing(Person::getSalary).reversed())
                .map(Person::getName).collect(Collectors.toList());
        // 先按工资再按年龄自然排序（从小到大）
        List<String> newList3 = Person.GetTestList().stream().sorted(Comparator.comparing(Person::getSalary).reversed())
                .sorted(Comparator.comparing(Person::getAge).reversed())
                .map(Person::getName).collect(Collectors.toList());
        // 先按工资再按年龄自定义排序（从大到小）
        List<String> newList4 = Person.GetTestList().stream().sorted((p1, p2) -> {
            if (p1.getSalary() == p2.getSalary()) {
                return p2.getAge() - p1.getAge();
            } else {
                return p2.getSalary() - p1.getSalary();
            }
        }).map(Person::getName).collect(Collectors.toList());

        System.out.println("按工资自然排序：" + newList);
        System.out.println("按工资降序排序：" + newList2);
        System.out.println("先按工资再按年龄自然排序：" + newList3);
        System.out.println("先按工资再按年龄自定义降序排序：" + newList4);



        String[] arr1 = { "a", "b", "c", "d" };
        String[] arr2 = { "d", "e", "f", "g" };

        Stream<String> stream1 = Stream.of(arr1);
        Stream<String> stream2 = Stream.of(arr2);
        // concat:合并两个流 distinct：去重
        List<String> newList6 = Stream.concat(stream1, stream2).distinct().collect(Collectors.toList());
        // limit：限制从流中获得前n个数据
        List<Integer> collect = Stream.iterate(1, x -> x + 2).limit(10).collect(Collectors.toList());
        // skip：跳过前n个数据
        List<Integer> collect2 = Stream.iterate(1, x -> x + 2).skip(1).limit(5).collect(Collectors.toList());

        System.out.println("流合并：" + newList6);
        System.out.println("limit：" + collect);
        System.out.println("skip：" + collect2);
    }

}
