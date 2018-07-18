# bluetimemeter
Take time's measures in your code easy. You can get measures or log it easy with this tool

Example of uses:

//Simple Measure in default log file<br/>
Measure.Start();<br/>
some lines of code<br/>
Measure.Stop();<br/>
Task.WaitAll();<br/><br/>



//Simple Measure in default log file and get measure<br/>
Measure.Start();<br/>
some lines of code<br/>
Task.WaitAll();<br/>
var resultMeasure =Measure.Stop().Result;<br/><br/>


//Multiples Measures, only in one get measure (all in log file)<br/>

Measure.Start();<br/>
some lines of code<br/>
Measure.Start();<br/>
another lines of code<br/>
Measure.Stop();<br/>
Task.WaitAll();<br/>
var resultMeasure =Measure.Stop().Result;<br/>
