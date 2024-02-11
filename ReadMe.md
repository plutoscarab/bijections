
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
  sequence of bytes (a word of symbols using an alphabet of size 256).

- Enumerating all possible values of a data structure, for testing or exploration. 
  Just map each natural number in sequence.

- Generating random values of a data structure for testing. Just generate a random natural number and map it.

## Supported bijections

|Natural|Integer|Pair of naturals|n-tuple of naturals|Ratio of naturals|List of naturals|Set of naturals|Word (example with hieroglyphics)|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|0|0|(0,0)|(0,0,0)|0|[]|[]|""|
|2|1|(1,0)|(0,1,0)|1/2|[0,0]|[0,1]|"𓀁"|
|3|-2|(0,2)|(1,0,0)|2|[1]|[1]|"𓀂"|
|5|-3|(2,0)|(0,1,1)|3/2|[2]|[2]|"𓀄"|
|7|-4|(1,2)|(0,2,0)|3|[3]|[3]|"𓀆"|
|10|5|(0,4)|(0,0,3)|3/5|[1,0]|[1,2]|"𓀉"|
|14|7|(4,0)|(1,1,1)|3/4|[0,2]|[0,3]|"𓀍"|
|20|10|(5,0)|(0,0,4)|3/8|[0,1,0]|[0,2,3]|"𓀓"|
|29|-15|(1,6)|(3,0,1)|7/4|[14]|[14]|"𓀜"|
|43|-22|(7,1)|(2,1,2)|13/5|[21]|[21]|"𓀪"|
|65|-33|(10,0)|(3,0,3)|7/6|[32]|[32]|"𓁀"|
|100|50|(9,4)|(1,4,2)|7/19|[1,0,2]|[1,2,5]|"𓁣"|
|156|78|(3,14)|(0,8,0)|10/23|[3,0,0]|[3,4,5]|"𓀀𓀛"|
|246|123|(15,6)|(5,1,4)|14/19|[6,4]|[6,11]|"𓀀𓁵"|
|391|-196|(13,14)|(6,0,6)|29/9|[195]|[195]|"𓀂𓀆"|
|625|-313|(30,4)|(10,0,4)|43/33|[312]|[312]|"𓀃𓁰"|
|1003|-502|(13,31)|(6,1,10)|45/17|[501]|[501]|"𓀆𓁪"|
|1614|807|(18,38)|(8,3,9)|64/83|[25,2]|[25,28]|"𓀋𓁍"|
|2602|1301|(46,25)|(1,0,23)|103/167|[20,15]|[20,36]|"𓀓𓀩"|
|4200|2100|(14,77)|(4,12,12)|32/115|[2,3,0,2]|[2,6,7,10]|"𓀟𓁧"|
|6785|-3393|(115,0)|(9,12,12)|99/86|[3392]|[3392]|"𓀴𓀀"|

These can be combined, for example, to map naturals to pairs of rationals, by mapping to pairs and then mapping
each element of the pair to a rational.

|Natural|Map to pair and then to rationals|Map to list and then to pairs|Map to list and then to integers, and format|
|:-:|:-:|:-:|:-:|
|1|(0, 1)|[(0, 0)]|$$1$$|
|2|(1, 0)|[(0, 0), (0, 0)]|$$n$$|
|5|(1/2, 0)|[(1, 0)]|$$2$$|
|12|(1/2, 1/2)|[(0, 0), (0, 0), (0, 1)]|$$-n^2$$|
|29|(1, 2/3)|[(4, 0)]|$$8$$|
|70|(1/3, 3)|[(1, 0), (0, 2)]|$$-1-2n$$|
|169|(1/5, 1)|[(6, 6)]|$$43$$|
|408|(1/2, 5/8)|[(0, 0), (0, 0), (0, 2), (0, 0)]|$$2n^2+n^3$$|
|985|(10/3, 1/3)|[(27, 3)]|$$247$$|
|2378|(1/6, 4/11)|[(5, 2), (0, 0)]|$$17+n$$|
|5741|(9/13, 4/11)|[(20, 55)]|$$1436$$|
|13860|(30/19, 0)|[(1, 0), (2, 3), (0, 1)]|$$-1+9n-n^2$$|
|33461|(7/12, 5/23)|[(77, 105)]|$$8366$$|
|80782|(31/19, 11/25)|[(4, 9), (0, 14)]|$$48-53n$$|
|195025|(7/5, 65/18)|[(51, 390)]|$$48757$$|
|470832|(62/27, 12/17)|[(1, 0), (0, 0), (1, 0), (1, 3), (0, 0)]|$$-1-n^2+6n^3+n^4$$|
|1136689|(46/17, 19/71)|[(699, 366)]|$$284173$$|
|2744210|(55/32, 54/37)|[(27, 17), (0, 17)]|$$509-77n$$|
|6625109|(79/62, 49/69)|[(1103, 1470)]|$$1656278$$|
|15994428|(20/73, 82/17)|[(0, 12), (4, 12), (3, 0)]|$$-39-70n-5n^2$$|
|38613965|(101/22, 89/243)|[(3191, 3022)]|$$9653492$$|

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