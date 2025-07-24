# HideTerminal

このソフトウェアはターミナルの表示が強制のGUIを持つツールからGUIを表示させないようにするツールです。

使用方法

cdコマンドでHideTerminal.exeファイルのあるdirectoryに移動します

以下のコマンドで起動したいソフトウェアの設定を行います。
```
HideTerminal -c "{実行ファイルを実行するdirectory}" -e "{実行ファイル}" -a "{実行時引数の指定}" 
```

同じディレクトリに`HideTerminalSetting.json`ができており以下のように記述されていたら問題ないです。

```json
{"CurrentDirectory":"-c指定したdirectory","ExecutePath":"-eで指定した実行ファイル","Arguments":"-aで指定した実行時引数"}
```

また、コマンドではなく`HideTerminalSetting.json`を編集して設定を変更することも可能です


## 注意点

`"`や`'`は省略可能ですが実行時引数などスペースで区切られている場合は正しく認識しない場合があります。
