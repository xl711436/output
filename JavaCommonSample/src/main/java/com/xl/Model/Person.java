package com.xl.Model;

import java.util.ArrayList;
import java.util.List;

public class Person {
    private String name;  // 姓名
    private int salary; // 薪资
    private int age; // 年龄
    private String sex; //性别
    private String area;  // 地区

    // 构造方法
    public Person(String name, int salary, int age,String sex,String area) {
        this.name = name;
        this.salary = salary;
        this.age = age;
        this.sex = sex;
        this.area = area;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getSalary() {
        return salary;
    }

    public void setSalary(int salary) {
        this.salary = salary;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public String getSex() {
        return sex;
    }

    public void setSex(String sex) {
        this.sex = sex;
    }

    public String getArea() {
        return area;
    }

    public void setArea(String area) {
        this.area = area;
    }

    public static List<Person> GetTestList()
    {
        List<Person> personList = new ArrayList<Person>();
        personList.add(new Person("Tom", 8900, 10,"male", "New York"));
        personList.add(new Person("Jack", 7000, 10,"male", "Washington"));
        personList.add(new Person("Lily", 7800, 10,"female", "Washington"));
        personList.add(new Person("Anni", 8200, 10,"female", "New York"));
        personList.add(new Person("Owen", 9500, 10,"male", "New York"));
        personList.add(new Person("Alisa", 7900, 10,"female", "New York"));
        return personList;
    }
}
