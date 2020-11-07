
export class test1 { 
    constructor(parm1: string) {
        console.log('test1:.' + parm1);
    }

    static statictest() :string  {
        console.log('statictest');
        return 'bbb';
    }
     funtest() :string  {
        console.log('funtest');
        return 'ccc';
    }
}