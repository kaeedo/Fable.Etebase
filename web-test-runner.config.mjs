// import { esbuildPlugin } from '@web/dev-server-esbuild';
// import { importMapsPlugin } from '@web/dev-server-import-maps';
import { playwrightLauncher } from '@web/test-runner-playwright';
import { fromRollup } from '@web/dev-server-rollup';
import rollupCommonjs from '@rollup/plugin-commonjs';

const commonjs = fromRollup(rollupCommonjs);

export default {
    browsers: [
        playwrightLauncher({
            product: 'firefox',

        })
    ],
    nodeResolve: true,
    plugins: [
        commonjs({
          include: [
            // the commonjs plugin is slow, list the required packages explicitly:
            '**/node_modules/etebase/**/*',
          ],
        }),
      ],
    // plugins: [
    //     importMapsPlugin({
    //         inject: {
    //             importMap: {
    //                 imports: { etebase: '/node_modules/etebase/dist/lib/Etebase.js' },
    //             },
    //         },
    //     }),
    //     esbuildPlugin()
    // ],
};
