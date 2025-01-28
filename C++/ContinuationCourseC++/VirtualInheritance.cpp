//#include <iostream>
//
//class BaseClass { public: int i; };
//
//                        class DerivedClass1 : virtual public BaseClass { public: int j; };
//                        class DerivedClass2 : virtual public BaseClass { public: int k; };
//                        class DerivedClass3 : public DerivedClass1, public DerivedClass2 { public: int sum; };
//
//int main()
//{
//    DerivedClass3 dObj3;
//    //dObj3.DerivedClass1::i = 10;
//    dObj3.i = 10;
//    dObj3.j = 20;
//    dObj3.k = 30;
//
//    //dObj3.sum = dObj3.DerivedClass1::i + dObj3.j + dObj3.k;
//    dObj3.sum = dObj3.i + dObj3.j + dObj3.k;
//    std::cout << dObj3.sum << std::endl;
//}