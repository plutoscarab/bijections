
# Bijections Between Natural Numbers and Other Data Types

## What is a natural number bijection?

The bijections implemented here are one-to-one mappings between natural numbers (including zero) and other data types
including signed integers, non-negative rational numbers, tuples of fixed size, lists of natural numbers, and
sequences of symbols from an alphabet.

For example, the string "Hello, world!" can be represented by the list of Unicode codepoint values (or ASCII values)
[72, 101, 108, 108, 111, 44, 32, 119, 111, 114, 108, 100, 33]. This list can be mapped, using a bijective mapping,
to the value 521326923876532544869691613767074.

The important attributes of this mapping are:
- Every list corresponds to a different value. The values are guaranteed to be different if the lists are different. 
- Every value corresponds to a unique list, including the empty list which has a value of 0.

## Why Bijections?

The number obtained from a bijective mapping can be used for several purposes:

- Serializing and deserializing the value. Map a data value to a natural number, and then map the number to a unique 
  sequence of bytes (a sequence of symbols using an alphabet of size 256).

- Enumerating all possible values of a data structure, for testing or exploration. 
  Just map each natural number in sequence.

- Generating random values of a data structure for testing. Just generate a random natural number and map it.

## Supported bijections

|Natural|Integer|Pair of naturals|n-tuple of naturals|Ratio of naturals|List of naturals|Word (base 10 example)|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|0|0|(0, 0)|(0, 0, 0)|0|[]|[]|
|1|-1|(0, 1)|(0, 0, 1)|1|[0]|[0]|
|2|1|(1, 0)|(0, 1, 0)|1/2|[0, 0]|[1]|
|3|-2|(0, 2)|(1, 0, 0)|2|[1]|[2]|
|5|-3|(2, 0)|(0, 1, 1)|3/2|[3]|[4]|
|8|4|(2, 1)|(1, 1, 0)|1/4|[6]|[7]|
|13|-7|(3, 1)|(0, 2, 1)|5/3|[0, 0, 0]|[0, 2]|
|21|-11|(0, 6)|(0, 1, 3)|8/5|[2, 1]|[1, 0]|
|34|17|(6, 1)|(4, 0, 0)|5/9|[21]|[2, 3]|
|55|-28|(0, 10)|(5, 0, 0)|11/3|[42]|[4, 4]|
|89|-45|(11, 1)|(2, 0, 5)|17/12|[0, 0, 3]|[7, 8]|
|144|72|(8, 8)|(3, 3, 2)|4/19|[10, 0]|[0, 3, 3]|
|233|-117|(2, 19)|(3, 1, 6)|25/18|[154]|[1, 2, 2]|
|377|-189|(26, 0)|(3, 1, 8)|29/20|[1, 0, 2]|[2, 6, 6]|
|610|305|(15, 19)|(5, 4, 5)|31/55|[14, 7]|[4, 9, 9]|
|987|-494|(41, 2)|(3, 2, 12)|52/19|[622]|[8, 7, 6]|
|1597|-799|(1, 55)|(2, 8, 10)|67/37|[1, 3, 6]|[0, 4, 8, 6]|
|2584|1292|(28, 43)|(8, 15, 0)|31/107|[1218]|[1, 4, 7, 3]|
|4181|-2091|(86, 4)|(1, 14, 13)|139/86|[2815]|[3, 0, 7, 0]|
|6765|-3383|(95, 20)|(10, 10, 13)|282/163|[50, 18]|[5, 6, 5, 4]|

These can be combined, for example, to map naturals to pairs of rationals, by mapping to pairs and then mapping
each element of the pair to a rational.

|Natural|Map to pair and then to rationals|Map to list and then to pairs|Map to list and then to integers, and format|
|:-:|:-:|:-:|:-:|
|1|(0, 1)|[(0, 0)]|$$1$$|
|2|(1, 0)|[(0, 0), (0, 0)]|$$n$$|
|5|(1/2, 0)|[(0, 2)]|$$-2$$|
|12|(1/2, 1/2)|[(0, 4)]|$$-3$$|
|29|(1, 2/3)|[(1, 4)]|$$1+2n$$|
|70|(1/3, 3)|[(2, 8)]|$$-n-n^2$$|
|169|(1/5, 1)|[(12, 0)]|$$-3+2n$$|
|408|(1/2, 5/8)|[(1, 0), (0, 1), (1, 0)]|$$-78$$|
|985|(10/3, 1/3)|[(25, 9)]|$$-3-2n^2$$|
|2378|(1/6, 4/11)|[(22, 22)]|$$1+2n+5n^2$$|
|5741|(9/13, 4/11)|[(1, 8), (2, 0)]|$$687$$|
|13860|(30/19, 0)|[(0, 0), (2, 3), (1, 0)]|$$9-n-2n^2$$|
|33461|(7/12, 5/23)|[(0, 3), (0, 2), (0, 1), (1, 0)]|$$3564$$|
|80782|(31/19, 11/25)|[(1, 0), (3, 0), (0, 0), (0, 3)]|$$n^8+n^{18}+n^{19}$$|
|195025|(7/5, 65/18)|[(3, 0), (3, 4), (3, 1)]|$$69+44n$$|
|470832|(62/27, 12/17)|[(7, 15), (15, 5)]|$$1-3n+2n^2-n^3+n^4-n^5$$|
|1136689|(46/17, 19/71)|[(200, 728)]|$$2+n^4+n^5+n^7+n^8+2n^9$$|
|2744210|(55/32, 54/37)|[(1, 1), (5, 1), (1, 4), (0, 1)]|$$3-n+2n^3-2n^7$$|
|6625109|(79/62, 49/69)|[(1387, 694)]|$$n+n^3+n^9-n^{10}+n^{13}-n^{16}$$|
|15994428|(20/73, 82/17)|[(4, 16), (3, 2), (6, 0)]|$$192+507n$$|
|38613965|(101/22, 89/243)|[(2, 2), (2, 0), (10, 0), (0, 5)]|$$-1-8n+n^4-6n^5$$|

Mappings can be recursively defined, for example to encode a term algebra to generate or encode
arbitrary expressions.

|Natural|Math expression|Code expression|
|:-:|:-:|:-:|
|1|$$y$$|`Str("")`|
|4|$$\sqrt{x}$$|`Str("a")`|
|21|$$\left(x+\pi\right)$$|`Int(7)`|
|104|$$\sqrt{\frac{x}{\pi}}$$|`Tree(Int(2), Str(""))`|
|521|$$\left(\left(x-y\right)+y\right)$$|`Tree(Tree(Int(0), Int(0)), Str("e"))`|
|2604|$$\sqrt{\frac{\left(x-y\right)}{y}}$$|`Int(868)`|
|13021|$$\left(\left(\pi-y\right)+\sqrt{\sqrt{x}}\right)$$|`Str("fjx")`|
|65104|$$\sqrt{\frac{\left(\pi-y\right)}{\sqrt{\sqrt{x}}}}$$|`Str("afbq")`|
|325521|$$\left(25+\left(y-2\right)\right)$$|`Int(108507)`|
|1627604|$$\sqrt{\frac{25}{\left(y-2\right)}}$$|`Tree(Tree(Tree(Int(0), Int(0)), Tree(Str(""), Int(0))), Str("kc"))`|
|8138021|$$\left(\left(\frac{x}{x}-\left(y-x\right)\right)+\left(\sqrt{y}+\sqrt{x}\right)\right)$$|`Tree(Str("uz"), Tree(Str("d"), Int(2)))`|
|40690104|$$\sqrt{\frac{\left(\frac{x}{x}-\left(y-x\right)\right)}{\left(\sqrt{y}+\sqrt{x}\right)}}$$|`Int(13563368)`|
|203450521|$$\left(1079+\left(\left(x+\pi\right)-\left(y+x\right)\right)\right)$$|`Str("erjlsz")`|
|1017252604|$$\sqrt{\frac{1079}{\left(\left(x+\pi\right)-\left(y+x\right)\right)}}$$|`Str("abmzkva")`|
|5086263021|$$\left(8929+\left(x+3\right)\right)$$|`Int(1695421007)`|
|25431315104|$$\sqrt{\frac{8929}{\left(x+3\right)}}$$|`Tree(Tree(Tree(Str("a"), Str("a")), Tree(Int(1), Int(2))), Tree(Int(31), Tree(Tree(Int(0), Int(0)), Str(""))))`|
|127156575521|$$\left(\left(\left(x+\pi\right)-\left(2+y\right)\right)+\sqrt{\sqrt{\frac{x}{3}}}\right)$$|`Tree(Tree(Tree(Int(1), Int(2)), Int(14)), Tree(Int(45), Str("ci")))`|
|635782877604|$$\sqrt{\frac{\left(\left(x+\pi\right)-\left(2+y\right)\right)}{\sqrt{\sqrt{\frac{x}{3}}}}}$$|`Int(211927625868)`|
|3178914388021|$$\left(\left(\frac{\sqrt{y}}{\frac{x}{x}}-1\right)+\sqrt{\sqrt{2305}}\right)$$|`Str("eaxdttapx")`|
|15894571940104|$$\sqrt{\frac{\left(\frac{\sqrt{y}}{\frac{x}{x}}-1\right)}{\sqrt{\sqrt{2305}}}}$$|`Str("yipwyvhfq")`|
|79472859700521|$$\left(814780+\left(\left(1+\sqrt{y}\right)-\sqrt{\frac{\sqrt{x}}{x}}\right)\right)$$|`Int(26490953233507)`|