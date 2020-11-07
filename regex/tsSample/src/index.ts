
import * as fs from 'fs';
import * as path from 'path'; 

import {regexSimple}   from '../src/regexExample';

//regexSimple.regexDefine();
//regexSimple.regexSimple();
//regexSimple.regexProperty();
//regexSimple.regexFunction();

//regexSimple.regexCommon();
//regexSimple.regexMore();
regexSimple.regexStringFunction();
//regexSimple.regexCatchGroup();
regexSimple.regexUseSample();
/*
let  mm:test1 = new test1('mm');
test1.statictest();
mm.funtest();


const bb = regex1.fun();
console.log(bb);

const hello: string = 'hello world';
const hello1: string = 'hello world1';
console.log(hello + hello1);


var re = /apples/gi; 
console.log(re.constructor);

let reg1:RegExp = new RegExp("/apples/gi");
console.log(reg1.constructor);
console.dir(reg1);

let reg5:RegExp = /\S/gi
let website:string = 'Jxsp anyg.com'
let result1:boolean = reg5.test(website);
console.log(reg5.lastIndex);
let result2:any = reg5.exec(website);
console.log(reg5.lastIndex);

let result3:any = reg5.exec(website);
console.log(reg5.source);
console.log(reg5.ignoreCase);
console.log(reg5.multiline);
console.log(reg5.global);

console.log(result1);
console.log(result2);
console.log(result3);
if(result2 != null)
{
	console.log(result2.constructor);
}



require('http').createServer((req, res) => {
    if (req.url === '/') {
      fs.createReadStream(
        path.join(__dirname, '../index.html')
      ).pipe(res);
    } else {
      res.end(req.url);
    }
  }).listen(8001, () => {
    console.log('run at 8001');
  });
  */