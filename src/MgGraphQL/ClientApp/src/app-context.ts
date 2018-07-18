import * as React from "react";
import ApolloClient from "apollo-boost";

export interface AppContextProps {
    sessionId: string | undefined;
    onSessionAcquired: (session: string) => void;
    client: ApolloClient<{}> | undefined;
}

function NOOP() {}

export const AppContext = React.createContext<AppContextProps>({
    sessionId: undefined,
    onSessionAcquired: NOOP,
    client: undefined
});