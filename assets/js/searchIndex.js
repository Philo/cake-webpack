
var camelCaseTokenizer = function (builder) {

  var pipelineFunction = function (token) {
    var previous = '';
    // split camelCaseString to on each word and combined words
    // e.g. camelCaseTokenizer -> ['camel', 'case', 'camelcase', 'tokenizer', 'camelcasetokenizer']
    var tokenStrings = token.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
      var current = cur.toLowerCase();
      if (acc.length === 0) {
        previous = current;
        return acc.concat(current);
      }
      previous = previous.concat(current);
      return acc.concat([current, previous]);
    }, []);

    // return token for each string
    // will copy any metadata on input token
    return tokenStrings.map(function(tokenString) {
      return token.clone(function(str) {
        return tokenString;
      })
    });
  }

  lunr.Pipeline.registerFunction(pipelineFunction, 'camelCaseTokenizer')

  builder.pipeline.before(lunr.stemmer, pipelineFunction)
}
var searchModule = function() {
    var documents = [];
    var idMap = [];
    function a(a,b) { 
        documents.push(a);
        idMap.push(b); 
    }

    a(
        {
            id:0,
            title:"WebpackGlobalRunner",
            content:"WebpackGlobalRunner",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Webpack/api/Cake.Webpack/WebpackGlobalRunner',
            title:"WebpackGlobalRunner",
            description:""
        }
    );
    a(
        {
            id:1,
            title:"WebpackBuildMode",
            content:"WebpackBuildMode",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Webpack/api/Cake.Webpack/WebpackBuildMode',
            title:"WebpackBuildMode",
            description:""
        }
    );
    a(
        {
            id:2,
            title:"WebpackLocalRunnerSettings",
            content:"WebpackLocalRunnerSettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Webpack/api/Cake.Webpack/WebpackLocalRunnerSettings',
            title:"WebpackLocalRunnerSettings",
            description:""
        }
    );
    a(
        {
            id:3,
            title:"WebpackRunnerSettings",
            content:"WebpackRunnerSettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Webpack/api/Cake.Webpack/WebpackRunnerSettings',
            title:"WebpackRunnerSettings",
            description:""
        }
    );
    a(
        {
            id:4,
            title:"NodeToolRunner",
            content:"NodeToolRunner",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Webpack/api/Cake.Webpack/NodeToolRunner_1',
            title:"NodeToolRunner<TSettings>",
            description:""
        }
    );
    a(
        {
            id:5,
            title:"WebpackRunnerFactory",
            content:"WebpackRunnerFactory",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Webpack/api/Cake.Webpack/WebpackRunnerFactory',
            title:"WebpackRunnerFactory",
            description:""
        }
    );
    a(
        {
            id:6,
            title:"WebpackLocalRunner",
            content:"WebpackLocalRunner",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Webpack/api/Cake.Webpack/WebpackLocalRunner',
            title:"WebpackLocalRunner",
            description:""
        }
    );
    a(
        {
            id:7,
            title:"WebpackRunner",
            content:"WebpackRunner",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Webpack/api/Cake.Webpack/WebpackRunner_1',
            title:"WebpackRunner<TSettings>",
            description:""
        }
    );
    a(
        {
            id:8,
            title:"WebpackRunnerAliases",
            content:"WebpackRunnerAliases",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Webpack/api/Cake.Webpack/WebpackRunnerAliases',
            title:"WebpackRunnerAliases",
            description:""
        }
    );
    var idx = lunr(function() {
        this.field('title');
        this.field('content');
        this.field('description');
        this.field('tags');
        this.ref('id');
        this.use(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
        documents.forEach(function (doc) { this.add(doc) }, this)
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
