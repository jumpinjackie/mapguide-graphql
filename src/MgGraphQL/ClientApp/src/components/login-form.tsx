import * as React from "react";
import Form from "antd/lib/form";
import Icon from "antd/lib/icon";
import Input from "antd/lib/input";
import Button from "antd/lib/button";
import ApolloClient, { gql } from "apollo-boost";

const FormItem = Form.Item;

const CREATE_SESSION = gql`
    mutation CreateSession($username: String!, $password: String!) {
        createSession(options: { username: $username, password: $password }) {
            data
        }
    }
`;

interface LoginFormProps {
    form: any;
    onSessionAcquired: (session: string) => void;
    client: ApolloClient<{}>;
}

class NormalLoginForm extends React.Component<LoginFormProps, any> {
    private handleSubmit = (e: any) => {
        e.preventDefault();
        const { form, onSessionAcquired, client } = this.props;
        form.validateFields(async (err: any, values: any) => {
            if (!err) {
                console.log('Received values of form: ', values);
                const result = await client.mutate<{}>({
                    mutation: CREATE_SESSION,
                    variables: {...values}
                });
                onSessionAcquired(result.data!["createSession"].data);
            }
        });
    }

    render() {
        const { getFieldDecorator } = this.props.form;
        return (
            <Form onSubmit={this.handleSubmit} className="login-form" style={{ padding: '10px' }}>
                <FormItem>
                    {getFieldDecorator('username', {
                        rules: [{ required: true, message: 'Please input your username!' }],
                    })(
                        <Input prefix={<Icon type="user" style={{ color: 'rgba(0,0,0,.25)' }} />} placeholder="Username" />
                    )}
                </FormItem>
                <FormItem>
                    {getFieldDecorator('password', {
                        rules: [{ required: true, message: 'Please input your Password!' }],
                    })(
                        <Input prefix={<Icon type="lock" style={{ color: 'rgba(0,0,0,.25)' }} />} type="password" placeholder="Password" />
                    )}
                </FormItem>
                <FormItem>
                    <Button type="primary" htmlType="submit" className="login-form-button">
                        Log in
                    </Button>
                </FormItem>
            </Form>
        );
    }
}

export const LoginForm = Form.create()(NormalLoginForm);