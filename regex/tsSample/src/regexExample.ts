export class regexSimple {
    constructor() {
    }

    // 正则的定义，有多种方式
    static regexDefine() {
        //RegExp 对象了解
        let reg1: RegExp = new RegExp('a');
        let testStr1: string = 'a';
        let testStr2: string = 'Abc';

        let result: boolean = reg1.test(testStr1);
        console.log(result);
        result = reg1.test(testStr2);
        console.log(result);

        let reg2: RegExp = new RegExp('a', 'i');
        result = reg2.test(testStr1);
        console.log(result);
        result = reg2.test(testStr2);
        console.log(result);

        let reg3 = /a/i;
        result = reg3.test(testStr1);
        console.log(result);
        result = reg3.test(testStr2);
        console.log(result);

        console.log(reg2.constructor.name);
        console.log(reg3.constructor.name);

        let reg4 = /\w/;
        let reg5: RegExp = new RegExp('\\w');
        //推荐的方法
        let reg6: RegExp = new RegExp(/\w/);
        console.log(reg4.source);
        console.log(reg5.source);
        console.log(reg6.source);
    }


    // 简单匹配
    static regexSimple() {
        let reg1: RegExp = new RegExp(/./);
        let testStr1: string = 'abc';
        let testStr2: string = 'mdl';
 
        let result: boolean = reg1.test(testStr1);
        console.log(result);

        result = reg1.test(testStr2);
        console.log(result);

        let reg2: RegExp = new RegExp(/\d/);
        let testStr3: string = 'ml235';
        result = reg2.test(testStr1);
        console.log(result);
        result = reg2.test(testStr3);
        console.log(result);
 
        let reg3: RegExp = new RegExp(/\w/);
        let testStr4: string = '34';
        result = reg3.test(testStr1);
        console.log(result);
        result = reg3.test(testStr4);
        console.log(result);

        let reg4: RegExp = new RegExp(/\s/);
        let testStr5: string = '3 4';
        result = reg4.test(testStr4);
        console.log(result);
        result = reg4.test(testStr5);
        console.log(result);

        let reg5: RegExp = new RegExp(/\b/);
        let testStr6: string = 'hello';
        let testStr7 = testStr6.replace(reg5, 'new');
        console.log(testStr7);

        let reg6: RegExp = new RegExp(/\b/g);
        let testStr8 = testStr6.replace(reg6, 'new');
        console.log(testStr8);

    }

    // 正则的属性
    static regexProperty() {
        let reg1: RegExp = new RegExp(/\s/g);

        console.log(reg1.source);
        console.log(reg1.flags);
        console.log(reg1.global);
        console.log(reg1.ignoreCase);
        console.log(reg1.multiline);
        console.log(reg1.lastIndex);
    }

    // 正则的函数
    static regexFunction() {
        let reg1: RegExp = new RegExp(/\d/);
        let testStr1: string = '2n3m5';

        let result: boolean = reg1.test(testStr1);
        console.log(result);

        let resultArray: RegExpExecArray | null = reg1.exec(testStr1);
        /* while(resultArray !=null)
         {
             console.log(resultArray.input);
             console.log(resultArray.index);
             resultArray = reg1.exec(testStr1);
         }
         */

        let reg2: RegExp = new RegExp(/\d/g);
        resultArray = reg2.exec(testStr1);
        while (resultArray != null) {
            console.log(resultArray.input);
            console.log(resultArray[0]);
            console.log(resultArray.index);
            resultArray = reg2.exec(testStr1);
        }
    }

    // string 相关方法
    static regexStringFunction() {

        let reg1: RegExp = new RegExp(/\w/);
        let testStr1:string='abc';
        console.log(testStr1.match(reg1));

        let reg11: RegExp = new RegExp(/\w/g);

        testStr1 =testStr1.replace(reg11,'替换');
        console.log(testStr1);

        let testStr2:string='a b c';
        let reg2: RegExp = new RegExp(/\s/);
        let resultArray: string[] = testStr2.split(reg2);
        resultArray.forEach(element => {
            console.log(element);
        });

        let testStr3:string='abcdefg';
        let reg3: RegExp = new RegExp(/bc/);
        let resultIndex: number = testStr3.search(reg3);
        console.log(resultIndex);

    }

    //重复量词
    static regexNumber() {

        let reg1: RegExp = new RegExp(/\d*/);
        let reg2: RegExp = new RegExp(/\d?/);
        let reg3: RegExp = new RegExp(/\d+/); 
        let reg4: RegExp = new RegExp(/\d{1,2}/);

        let testStr1: string = 'ab';
        let testStr2: string = 'a3b';
        let testStr3: string = 'a33b';
        let testStr4: string = 'a333b'; 

        let result: boolean = reg1.test(testStr1);
        console.log(result);

        result = reg2.test(testStr1);
        console.log(result);
        result = reg3.test(testStr1);
        console.log(result);
        result = reg4.test(testStr1);
        console.log(result);

        result = reg1.test(testStr2);
        console.log(result);
        result = reg2.test(testStr2);
        console.log(result);
        result = reg3.test(testStr2);
        console.log(result);
        result = reg4.test(testStr2);
        console.log(result);

        result = reg1.test(testStr3);
        console.log(result);
        result = reg2.test(testStr3);
        console.log(result);
        result = reg3.test(testStr3);
        console.log(result);
        result = reg4.test(testStr3);
        console.log(result);

        result = reg1.test(testStr4);
        console.log(result);
        result = reg2.test(testStr4);
        console.log(result);
        result = reg3.test(testStr4);
        console.log(result);
        result = reg4.test(testStr4);
        console.log(result); 
    }

 
    // 常用的正则
    static regexCommon() { 
        let callNumber: RegExp = new RegExp(/^\d{3}-\d{8}|^\d{4}-\d{8}/);
        let testStr1: string = '027-34545877';
        let testStr2: string = '0723-87685877';
        let testStr21: string = '33232-87685877';
        let result: boolean = callNumber.test(testStr1);
        console.log(result);
        result = callNumber.test(testStr2);
        console.log(result);
        result = callNumber.test(testStr21);
        console.log(result);

        let qq: RegExp = new RegExp(/[1-9][0-9]{4,}/);

        let testStr3: string = '34564';
        result = qq.test(testStr3);
        console.log(result);
        let testStr4: string = 'aab3';
        result = qq.test(testStr4);
        console.log(result);

        let url: RegExp = new RegExp(/[a-zA-z]+:\/\/[^\s]*/);

        let testStr5: string = 'http://www.baidu.com';
        result = url.test(testStr5);
        console.log(result);
        let testStr6: string = 'ftp://10.10.10.10/abc.zip';
        result = url.test(testStr6);
        console.log(result);



    }

    //贪婪匹配         
    static regexMore() {
        let moreReg: RegExp = new RegExp(/[0-9]{4,}/);
        let lessReg: RegExp = new RegExp(/[0-9]{4,}?/);

        let testStr1: string = '334534453344';

        let resultArray: RegExpExecArray | null = moreReg.exec(testStr1);

        if (resultArray != null) {
            console.log(resultArray.input);
            console.log(resultArray[0]);
            console.log(resultArray.index); 
        }
    
 
        resultArray = lessReg.exec(testStr1);
        while (resultArray != null) {
            console.log(resultArray.input);
            console.log(resultArray[0]);
            console.log(resultArray.index); 
        } 
    }
 

    //捕获和分组           
    static regexCatchGroup() {
        let catchReg: RegExp = new RegExp(/123(\d)456/);
        let testStr1: string = 'aab1236456';

        let resultArray: RegExpExecArray | null = catchReg.exec(testStr1);
       
        if (resultArray != null) {
            console.log(resultArray.input);
            console.log(resultArray[0]);
            console.log(resultArray.index); 
        }

        let catchReg2: RegExp = new RegExp(/123(\d)456(\d)789/);
        let testStr2: string = 'aab12364563789';

         resultArray = catchReg2.exec(testStr2);
       
        if (resultArray != null) {
            console.log(resultArray.input);
            console.log(resultArray[0]);
            console.log(resultArray.index); 
        } 
        console.dir(RegExp);  
    }

    // 引用
    static regexRefrence() {
        let catchReplaceReg1: RegExp = new RegExp(/<(\/?)\w>/g);
        let testStr1: string = '<p>111</p><a>222</a>';
        let bb = catchReplaceReg1.test(testStr1);
        console.log(testStr1);
        testStr1 = testStr1.replace(catchReplaceReg1,'<' + '$1' + 'p>');
        console.log(testStr1); 
    }

    //  引用替换示例
    static regexUseSample()
    {
        let Reg1: RegExp = new RegExp(/(?<=\"-I)(.*)(?=\")/g);
        let Reg2: RegExp = new RegExp(/((?<=\s-I).*(?=\s+-))/g);
        let testStr1: string = '"-I/aabbcc"';
        let testStr2: string = ' -I/ccbbaa -';
        testStr1 = testStr1.replace(Reg1,'"$1"');
        console.log(testStr1); 
        testStr2 = testStr2.replace(Reg2,'"$1"');
        console.log(testStr2); 
    }
 

}


