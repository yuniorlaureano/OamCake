const path = require("path");
const glob = require("glob");

function getWebpackEntries(dir) {
    let entries = {};    
    glob.sync(dir).map((file) => {
        const dirname = path.basename(path.dirname(file));
        entries[dirname] = file;
    });

    return entries;
}

const entries = getWebpackEntries("./src/Pages/**/index.js");

module.exports = {
    devtool: 'source-map',
    entry: entries,
    output: {
        path: path.resolve(__dirname, '../wwwroot/js/app'),
    },
    resolve: {
        alias: {
            src: path.resolve(__dirname, "src"),
            components: path.resolve(__dirname, "src", "component")
        }
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader",
                    options: {
                        presets: [
                            "@babel/preset-env",
                            "@babel/preset-react"
                        ],
                    }
                }
            },
        ]
    },
    optimization: {
        splitChunks: {
            cacheGroups: {
            vendor: {
                test: /node_modules/,
                chunks: "initial",
                name: "vendor",
                enforce: true
            }
            }
        }
    }
};

