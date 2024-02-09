
# Bijections Between Natural Numbers and Other Data Types

Based on the paper "Bijective Size-preserving Gödel Numberings for Term Algebras" by Paul Tarau,
Department of Computer Science and Engineering, University of North Texas

## What is a natural number bijection?

The bijections implemented here are one-to-one mappings between natural numbers (including zero) and other data types
including signed integers, non-negative rational numbers, tuples of fixed size, lists of natural numbers, and
sequences of symbols from an alphabet.

For example, the string "Hello, world!" can be represented by the list of Unicode codepoint values (or ASCII values)
[72, 101, 108, 108, 111, 44, 32, 119, 111, 114, 108, 100, 33]. This list can be mapped, using a bijective mapping,
to the value 521326923876532544869691613767074.

The important attributes of these mappings are:
- Every valid value maps to a different natural. The natural numbers are guaranteed to be different if the data is different. 
- Every natural maps to a different valid value, including an "empty" or nil value which is usually mapped to 0.
- The size, in bits, of the natural scales in a reasonable way to the size, in bits, of the original data.

## Why Bijections?

The number obtained from a bijective mapping can be used for several purposes:

- Serializing and deserializing the value. Map a data value to a natural number, and then map the number to a unique 
  sequence of bytes (a sequence of symbols using an alphabet of size 256).

- Enumerating all possible values of a data structure, for testing or exploration. 
  Just map each natural number in sequence.

- Generating random values of a data structure for testing. Just generate a random natural number and map it.

## Supported bijections

|Natural|Integer|Pair of naturals|n-tuple of naturals|Ratio of naturals|List of naturals|Set of naturals|String (with hieroglyphic alphabet)|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|0|0|(0,0)|(0,0,0)|0|[]|[]|""|
|1|-1|(0,1)|(0,0,1)|1|[0]|[0]|"𓀀"|
|2|1|(1,0)|(0,1,0)|1/2|[0,0]|[0,1]|"𓀁"|
|3|-2|(0,2)|(1,0,0)|2|[1]|[1]|"𓀂"|
|5|-3|(2,0)|(0,1,1)|3/2|[3]|[3]|"𓀄"|
|8|4|(2,1)|(1,1,0)|1/4|[6]|[6]|"𓀇"|
|13|-7|(3,1)|(0,2,1)|5/3|[0,0,0]|[0,1,2]|"𓀌"|
|21|-11|(0,6)|(0,1,3)|8/5|[2,1]|[2,4]|"𓀔"|
|34|17|(6,1)|(4,0,0)|5/9|[21]|[21]|"𓀡"|
|55|-28|(0,10)|(5,0,0)|11/3|[42]|[42]|"𓀶"|
|89|-45|(11,1)|(2,0,5)|17/12|[0,0,3]|[0,1,5]|"𓁘"|
|144|72|(8,8)|(3,3,2)|4/19|[10,0]|[10,11]|"𓀀𓀏"|
|233|-117|(2,19)|(3,1,6)|25/18|[154]|[154]|"𓀀𓁨"|
|377|-189|(26,0)|(3,1,8)|29/20|[1,0,2]|[1,2,5]|"𓀁𓁸"|
|610|305|(15,19)|(5,4,5)|31/55|[14,7]|[14,22]|"𓀃𓁡"|
|987|-494|(41,2)|(3,2,12)|52/19|[622]|[622]|"𓀆𓁚"|
|1597|-799|(1,55)|(2,8,10)|67/37|[1,3,6]|[1,5,12]|"𓀋𓀼"|
|2584|1292|(28,43)|(8,15,0)|31/107|[1218]|[1218]|"𓀓𓀗"|
|4181|-2091|(86,4)|(1,14,13)|139/86|[2815]|[2815]|"𓀟𓁔"|
|6765|-3383|(95,20)|(10,10,13)|282/163|[50,18]|[50,69]|"𓀳𓁬"|


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
|1|$$y$$|`tree (leaf 0) (leaf 0)`|
|4|$$\sqrt{x}$$|`leaf 2`|
|23|$$5$$|`tree (tree (leaf 0) (leaf 0)) (tree (leaf 0) (tree (leaf 0) (leaf 0)))`|
|127|$$\left(1-1\right)$$|`tree (leaf 4) (leaf 1)`|
|704|$$\sqrt{\frac{\left(x+x\right)}{x}}$$|`leaf 352`|
|3901|$$\left(8+x\right)$$|`tree (tree (tree (leaf 0) (leaf 0)) (leaf 3)) (leaf 1)`|
|21617|$$\left(\sqrt{2}-10\right)$$|`tree (tree (leaf 1) (leaf 3)) (tree (leaf 3) (tree (leaf 0) (leaf 0)))`|
|119788|$$23958$$|`leaf 59894`|
|663791|$$\left(\left(y-\left(x+y\right)\right)+\left(x-\left(x+x\right)\right)\right)$$|`tree (leaf 95) (leaf 312)`|
|3678319|$$\sqrt{147133}$$|`tree (leaf 378) (tree (tree (tree (leaf 0) (tree (leaf 0) (leaf 0))) (leaf 0)) (leaf 7))`|
|20382968|$$4076594$$|`leaf 10191484`|
|112949797|$$\left(\left(\sqrt{\pi}-y\right)-\sqrt{242}\right)$$|`tree (leaf 1510) (tree (leaf 31) (leaf 12))`|
|625897889|$$\sqrt{\left(\sqrt{\left(\frac{x}{y}-2\right)}-\left(\frac{x}{x}+\left(y+y\right)\right)\right)}$$|`tree (tree (leaf 40) (tree (leaf 1) (leaf 1))) (leaf 6863)`|
|3468338836|$$\left(\frac{\frac{x}{\sqrt{x}}}{\left(y+y\right)}+\left(\left(\pi+\pi\right)+5\right)\right)$$|`leaf 1734169418`|
|19219387847|$$\left(6042-\left(\left(y-x\right)+\sqrt{\left(y+y\right)}\right)\right)$$|`tree (leaf 35131) (tree (tree (tree (leaf 0) (leaf 1)) (leaf 4)) (tree (tree (leaf 0) (leaf 0)) (leaf 0)))`|
|106501955743|$$21300391149$$|`tree (leaf 142593) (tree (leaf 38) (leaf 63))`|
|590167942256|$$\left(\sqrt{\left(\sqrt{\left(y+x\right)}-\left(\frac{x}{x}-x\right)\right)}+\left(\sqrt{\pi}-\left(y+\sqrt{x}\right)\right)\right)$$|`leaf 295083971128`|
|3270345578509|$$\sqrt{\left(\sqrt{\sqrt{\sqrt{\frac{\left(x-1\right)}{\pi}}}}+\frac{\pi}{\sqrt{x}}\right)}$$|`tree (tree (tree (leaf 3) (tree (tree (leaf 0) (leaf 0)) (tree (leaf 0) (leaf 0)))) (leaf 160)) (leaf 739500)`|
|18122231719313|$$3624446343863$$|`tree (leaf 2045473) (leaf 83037)`|
|100422195332092|$$\left(\left(\frac{\sqrt{x}}{\left(x-y\right)}+\frac{3}{\pi}\right)-\frac{\left(\pi+\left(x+y\right)\right)}{\sqrt{\frac{\pi}{\pi}}}\right)$$|`leaf 50211097666046`|
|556477671818399|$$\sqrt{\sqrt{\frac{\frac{\left(\left(x+x\right)-\frac{x}{x}\right)}{115}}{\left(\frac{\pi}{\frac{x}{y}}+\sqrt{\sqrt{\frac{x}{x}}}\right)}}}$$|`tree (leaf 5534834) (leaf 6260055)`|